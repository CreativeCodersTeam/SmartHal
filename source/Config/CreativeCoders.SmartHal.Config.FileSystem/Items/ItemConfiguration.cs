using System;
using CreativeCoders.SmartHal.Config.Base.Items;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.FileSystem.Items
{
    [PublicAPI]
    public class ItemConfiguration : ConfigurationObjectSettingsBase, IItemConfiguration
    {
        public string ItemType { get; set; }

        public string DataSource { get; set; }

        public string[] ChannelIds { get; set; } = Array.Empty<string>();
    }
}