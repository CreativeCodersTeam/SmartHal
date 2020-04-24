using CreativeCoders.SmartHal.Config.Base;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.FileSystem
{
    [PublicAPI]
    public abstract class ConfigurationObjectBase : IConfigurationObject
    {
        public string Name { get; set; }
    }
}