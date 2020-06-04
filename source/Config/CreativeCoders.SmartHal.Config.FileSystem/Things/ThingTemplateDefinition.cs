using System;
using CreativeCoders.SmartHal.Config.Base.Things;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.FileSystem.Things
{
    [PublicAPI]
    public class ThingTemplateDefinition : ConfigurationObjectSettingsBase, IThingTemplateDefinition
    {
        public string[] Channels { get; set; } = Array.Empty<string>();
    }
}