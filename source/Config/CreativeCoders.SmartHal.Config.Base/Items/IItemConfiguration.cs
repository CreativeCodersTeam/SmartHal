﻿using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.Base.Items
{
    [PublicAPI]
    public interface IItemConfiguration : IConfigurationObject, IConfigurationObjectSettings
    {
        string ItemType { get; }
        
        string DataSource { get; }

        string[] ChannelIds { get; }
    }
}