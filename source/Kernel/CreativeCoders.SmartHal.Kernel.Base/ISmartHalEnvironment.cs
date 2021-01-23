using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base
{
    [PublicAPI]
    public interface ISmartHalEnvironment
    {
        string InstanceConfigPath { get; }
    }
}