using CreativeCoders.SmartHal.Config.FileSystem.Things;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.FileSystem.Building.ConfigurationInitializers
{
    [UsedImplicitly]
    public class ThingTemplateConfigurationInitializer : ConfigurationInitializerBase<ThingTemplateDefinition>
    {
        public ThingTemplateConfigurationInitializer(string basePath)
            : base(basePath, "things", "*.template")
        {
        }
    }
}