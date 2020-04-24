using CreativeCoders.SmartHal.Config.Base.Things;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.FileSystem.Things
{
    [PublicAPI]
    public class ThingConfiguration : ConfigurationObjectSettingsBase, IThingConfiguration
    {
        public string GatewayName { get; set; }
        
        public string Address { get; set; }
        
        public string Template { get; set; }
    }
}