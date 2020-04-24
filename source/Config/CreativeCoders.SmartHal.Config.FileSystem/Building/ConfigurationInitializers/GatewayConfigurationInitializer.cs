using CreativeCoders.SmartHal.Config.FileSystem.Things;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.FileSystem.Building.ConfigurationInitializers
{
    [UsedImplicitly]
    public class GatewayConfigurationInitializer : ConfigurationInitializerBase<GatewayConfiguration>
    {
        public GatewayConfigurationInitializer(string basePath) : base(basePath, "gateways", "*.gateway")
        {
        }
    }
}