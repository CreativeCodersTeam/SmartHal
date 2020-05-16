using System.Collections.Generic;
using CreativeCoders.SmartHal.Config.Base.Things;

namespace CreativeCoders.SmartHal.Kernel.Base.SubSystems
{
    public class GatewayConfigurationPackage
    {
        public GatewayConfigurationPackage(IGatewayConfiguration gatewayConfiguration,
            IEnumerable<IThingConfiguration> thingConfigurations)
        {
            GatewayConfiguration = gatewayConfiguration;
            ThingConfigurations = thingConfigurations;
        }
        
        public IGatewayConfiguration GatewayConfiguration { get; }
        
        public IEnumerable<IThingConfiguration> ThingConfigurations { get; }
    }
}