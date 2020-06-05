using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreativeCoders.Config.Base;
using CreativeCoders.SmartHal.Config.Base.Things;
using CreativeCoders.SmartHal.Kernel.Base.InitSystem;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things
{
    [UsedImplicitly]
    [InitSystemStep(typeof(IThingSubSystem))]
    public class ThingsBootStep : IBootStep
    {
        private readonly IEnumerable<IThingTemplateDefinition> _thingTemplateDefinitions;
        
        private readonly IEnumerable<IGatewayConfiguration> _gatewayConfigurations;
        
        private readonly IEnumerable<IThingConfiguration> _thingConfigurations;
        
        private readonly IThingSubSystem _thingSubSystem;
        
        public ThingsBootStep(ISettings<IThingTemplateDefinition> thingTemplateDefinitions,
            ISettings<IGatewayConfiguration> gatewayConfigurations,
            ISettings<IThingConfiguration> thingConfigurations,
            IThingSubSystem thingSubSystem)
        {
            _thingTemplateDefinitions = thingTemplateDefinitions.Values;
            _gatewayConfigurations = gatewayConfigurations.Values;
            _thingConfigurations = thingConfigurations.Values;
            _thingSubSystem = thingSubSystem;
        }
        
        public async Task ExecuteAsync()
        {
            var gatewayConfigurationPackages = _gatewayConfigurations
                .GroupJoin(_thingConfigurations,
                    gc => gc.Name,
                    tc => tc.GatewayName,
                    (gatewayConfiguration, thingConfigurations) =>
                        new GatewayConfigurationPackage(gatewayConfiguration, thingConfigurations));

            await _thingSubSystem.InitThingTemplatesAsync(_thingTemplateDefinitions).ConfigureAwait(false);

            await _thingSubSystem.InitGatewayConfigurationPackagesAsync(gatewayConfigurationPackages);
        }
    }
}