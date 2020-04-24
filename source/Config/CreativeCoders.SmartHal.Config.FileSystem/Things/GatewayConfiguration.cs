using CreativeCoders.SmartHal.Config.Base.Things;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.FileSystem.Things
{
    [PublicAPI]
    public class GatewayConfiguration : ConfigurationObjectSettingsBase, IGatewayConfiguration
    {
        public string GatewayType { get; set; }
        
        public string DriverName { get; set; }
        
        public string Address { get; set; }
    }
}