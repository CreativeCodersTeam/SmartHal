using CreativeCoders.SmartHal.Config.FileSystem.Things;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.FileSystem.Building.ConfigurationInitializers
{
    [UsedImplicitly]
    public class ThingConfigurationInitializer : ConfigurationInitializerBase<ThingConfiguration>
    {
        public ThingConfigurationInitializer(string basePath) : base(basePath, "things", "*.thing")
        {
        }
    }
}