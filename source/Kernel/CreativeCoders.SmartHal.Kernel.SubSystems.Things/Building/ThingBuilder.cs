using System.Linq;
using System.Threading.Tasks;
using CreativeCoders.Core.Logging;
using CreativeCoders.SmartHal.Config.Base.Things;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.Things;
using CreativeCoders.SmartHal.Kernel.SubSystems.Things.Building.SetupInfos;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things.Building
{
    [UsedImplicitly]
    public class ThingBuilder : IThingBuilder
    {
        private static readonly ILogger Log = LogManager.GetLogger<ThingBuilder>();
        
        private readonly IGatewayRepository _gatewayRepository;
        
        private readonly IThingRepository _thingRepository;
        
        private readonly IThingTemplateRepository _thingTemplateRepository;
        
        private readonly IThingChannelBuilder _thingChannelBuilder;
        
        private readonly IMessageHub _messageHub;

        public ThingBuilder(IGatewayRepository gatewayRepository, IThingRepository thingRepository,
            IThingTemplateRepository thingTemplateRepository, IThingChannelBuilder thingChannelBuilder, IMessageHub messageHub)
        {
            _gatewayRepository = gatewayRepository;
            _thingRepository = thingRepository;
            _thingTemplateRepository = thingTemplateRepository;
            _thingChannelBuilder = thingChannelBuilder;
            _messageHub = messageHub;
        }
        
        public async Task<Thing> Build(IThingConfiguration thingConfiguration)
        {
            var gateway = _gatewayRepository.FirstOrDefault(x => x.Name == thingConfiguration.GatewayName);

            if (gateway == null)
            {
                Log.Warn($"Gateway '{thingConfiguration.GatewayName}' for thing '{thingConfiguration.Name}' not found");
                
                return null;
            }
            
            var thingTemplate = _thingTemplateRepository.GetTemplate(thingConfiguration.Template);
            
            return await CreateThing(thingConfiguration, thingTemplate, gateway);
        }

        private async Task<Thing> CreateThing(IThingConfiguration thingConfiguration, IThingTemplate thingTemplate, IGateway gateway)
        {
            var thing = new Thing(thingConfiguration.Name, gateway.Id, thingTemplate, _thingChannelBuilder, _messageHub);
            
            var thingHandler = gateway.CreateThingHandler(new ThingSetupInfo(thingConfiguration, thingTemplate, thing));
            
            if (thingHandler == null)
            {
                Log.Error($"Gateway '{thingConfiguration.GatewayName}' does not create thing handler for thing '{thingConfiguration.Name}'");
                
                return null;
            }
            
            Log.Info($"Thing handler for thing '{thingConfiguration.Name}' created");
            
            thing.SetHandler(thingHandler);
            
            await thingHandler.SetupAsync(_messageHub).ConfigureAwait(false);
            
            await _thingRepository.AddAsync(thing).ConfigureAwait(false);

            return thing;
        } 
    }
}