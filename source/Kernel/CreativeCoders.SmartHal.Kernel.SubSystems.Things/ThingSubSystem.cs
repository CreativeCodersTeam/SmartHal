using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.SmartHal.Config.Base.Things;
using CreativeCoders.SmartHal.Kernel.Base.Messages.Things;
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
    [SubSystem("Things")]
    [DependsOn(typeof(IDriverSubSystem))]
    public class ThingSubSystem : SubSystemBase, IThingSubSystem
    {
        private readonly IGatewayBuilder _gatewayBuilder;
        
        private readonly IThingBuilder _thingBuilder;

        private readonly IThingTemplateRepository _thingTemplateRepository;
        
        private readonly IKernelRequestDispatcher _requestDispatcher;
        
        private readonly IMessageHub _messageHub;

        private readonly IDictionary<GatewayConfigurationPackage, IDisposable> _gatewayInitializedHandlers;

        public ThingSubSystem(IGatewayBuilder gatewayBuilder, IThingBuilder thingBuilder,
            IThingTemplateRepository thingTemplateRepository, IKernelRequestDispatcher requestDispatcher,
            IMessageHub messageHub, IGatewayRepository gatewayRepository)
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
            var gateway = await _gatewayBuilder.BuildAsync(gatewayConfiguration).ConfigureAwait(false);

            if (gateway == null)
            {
                return;
            }

            afterGatewayCreation(gateway);
            
            _requestDispatcher.Process(new KernelRequest($"InitGateway {gateway.Id}", () => gateway.InitAsync()));
        }

        public async Task InitThingTemplatesAsync(IEnumerable<IThingTemplateDefinition> thingTemplateDefinitions)
        {
            await thingTemplateDefinitions
                .ForEachAsync(
                    async x =>
                        await _thingTemplateRepository.AddAsync(new ThingTemplate(x)).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async Task InitGatewayConfigurationPackagesAsync(IEnumerable<GatewayConfigurationPackage> gatewayConfigurationPackages)
        {
            await gatewayConfigurationPackages
                .ForEachAsync(
                    async x =>
                        await InitGatewayConfigurationPackage(x).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        private async Task InitThingAsync(IThingConfiguration thingConfiguration)
        {
            var thing = await _thingBuilder.BuildAsync(thingConfiguration).ConfigureAwait(false);

            if (thing == null)
            {
                return;
            }

            _requestDispatcher.Process(new KernelRequest($"InitThing {thing.Id}", () => thing.InitAsync()));
        }

        private async Task InitGatewayConfigurationPackage(GatewayConfigurationPackage gatewayConfigurationPackage)
        {
            await InitGatewayAsync(gatewayConfigurationPackage.GatewayConfiguration,
                gateway => RegisterThingsInitHandler(gateway, gatewayConfigurationPackage))
                .ConfigureAwait(false);
        }

        private void RegisterThingsInitHandler(IGateway gateway, GatewayConfigurationPackage gatewayConfigurationPackage)
        {
            var initThingsHandler = _messageHub
                .Handle<ThingStateChangedMessage>()
                .Where(msg => gateway.Id.Equals(msg.Id) && msg.NewState.IsInitialized())
                .Once()
                .Register(_ => InitThingsAsync(gatewayConfigurationPackage));

            _gatewayInitializedHandlers[gatewayConfigurationPackage] = initThingsHandler;
        }

        private async Task InitThingsAsync(GatewayConfigurationPackage gatewayConfigurationPackage)
        {
            if (!_gatewayInitializedHandlers.Remove(gatewayConfigurationPackage, out var handler))
            {
                return;
            }
            
            handler.Dispose();
            
            await gatewayConfigurationPackage
                .ThingConfigurations
                .ForEachAsync(InitThingAsync)
                .ConfigureAwait(false);
        }
    }
}