using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.Base
{
    [PublicAPI]
    public interface IConfigurationObject
    {
        string Name { get; }
    }
}