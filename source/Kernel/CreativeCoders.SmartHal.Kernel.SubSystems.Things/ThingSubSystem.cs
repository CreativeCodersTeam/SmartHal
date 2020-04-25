using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.SmartHal.Config.Base.Things;
using CreativeCoders.SmartHal.Kernel.Base.Messages;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.Requests;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using CreativeCoders.SmartHal.Kernel.Base.Things;
using CreativeCoders.SmartHal.Kernel.SubSystems.Things.Building;
using CreativeCoders.SmartHal.Kernel.SubSystems.Things.Templates;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things
{
    [UsedImplicitly]
    public class ThingSubSystem : IThingSubSystem
    {
        private readonly IGatewayBuilder _gatewayBuilder;
        
        private readonly IThingBuilder _thingBuilder;

        private readonly IThingTemplateRepository _thingTemplateRepository;
        
        private readonly IKernelRequestDispatcher _requestDispatcher;
        
        private readonly IMessageHub _messageHub;

        private readonly IDictionary<GatewayConfigurationPackage, IDisposable> _gatewayInitializedHandlers;

        public ThingSubSystem(IGatewayBuilder gatewayBuilder, IThingBuilder thingBuilder,
            IThingTemplateRepository thingTemplateRepository, IKernelRequestDispatcher requestDispatcher,
            IMessageHub messageHub)
        {
            _gatewayBuilder = gatewayBuilder;
            _thingBuilder = thingBuilder;
            _thingTemplateRepository = thingTemplateRepository;
            _requestDispatcher = requestDispatcher;
            _messageHub = messageHub;
            
            _gatewayInitializedHandlers = new ConcurrentDictionary<GatewayConfigurationPackage, IDisposable>();
        }

        private async Task InitGatewayAsync(IGatewayConfiguration gatewayConfiguration, Action<IGateway> afterGatewayCreation)
        {
            var gateway = await _gatewayBuilder.Build(gatewayConfiguration);

            if (gateway == null)
            {
                return;
            }

            afterGatewayCreation(gateway);
            
            _requestDispatcher.Process(new KernelRequest($"InitGateway {gateway.Id}", () => gateway.InitAsync()));
        }

        public Task InitThingTemplatesAsync(IEnumerable<IThingTemplateDefinition> thingTemplateDefinitions)
        {
            return thingTemplateDefinitions
                .ForEachAsync(x => _thingTemplateRepository.AddAsync(new ThingTemplate(x)));
        }

        public Task InitGatewayConfigurationPackagesAsync(IEnumerable<GatewayConfigurationPackage> gatewayConfigurationPackages)
        {
            return gatewayConfigurationPackages.ForEachAsync(InitGatewayConfigurationPackage);
        }

        private async Task InitThingAsync(IThingConfiguration thingConfiguration)
        {
            var thing = await _thingBuilder.Build(thingConfiguration);

            if (thing == null)
            {
                return;
            }

            _requestDispatcher.Process(new KernelRequest($"InitThing {thing.Id}", () => thing.InitAsync()));
        }

        private Task InitGatewayConfigurationPackage(GatewayConfigurationPackage gatewayConfigurationPackage)
        {
            return InitGatewayAsync(gatewayConfigurationPackage.GatewayConfiguration,
                gateway => RegisterThingsInitHandler(gateway, gatewayConfigurationPackage));
        }

        private void RegisterThingsInitHandler(IGateway gateway, GatewayConfigurationPackage gatewayConfigurationPackage)
        {
            var initThingsHandler = _messageHub
                .Handle<ThingStateChangedMessage>()
                .Where(msg => gateway.Id.Equals(msg.Id) && msg.NewState.IsInitialized())
                .Once()
                .Register(msg => InitThingsAsync(gatewayConfigurationPackage));

            _gatewayInitializedHandlers[gatewayConfigurationPackage] = initThingsHandler;
        }

        private Task InitThingsAsync(GatewayConfigurationPackage gatewayConfigurationPackage)
        {
            if (!_gatewayInitializedHandlers.Remove(gatewayConfigurationPackage, out var handler))
            {
                return Task.CompletedTask;
            }
            
            handler.Dispose();
            
            return gatewayConfigurationPackage.ThingConfigurations.ForEachAsync(InitThingAsync);
        }
    }
}