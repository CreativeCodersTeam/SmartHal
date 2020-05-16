using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base
{
    [PublicAPI]
    public enum KernelState
    {
        None,
        Initializing,
        Initialized,
        Booting,
        Running,
        ShuttingDown,
        ShutDown
    }
}