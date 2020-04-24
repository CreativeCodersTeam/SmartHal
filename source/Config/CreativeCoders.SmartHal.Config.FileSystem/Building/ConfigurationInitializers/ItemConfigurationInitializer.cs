using CreativeCoders.SmartHal.Config.FileSystem.Items;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.FileSystem.Building.ConfigurationInitializers
{
    [UsedImplicitly]
    public class ItemConfigurationInitializer : ConfigurationInitializerBase<ItemConfiguration>
    {
        public ItemConfigurationInitializer(string basePath) : base(basePath, "items", "*.item")
        {
        }
    }
}