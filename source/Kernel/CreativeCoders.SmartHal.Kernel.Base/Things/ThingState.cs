using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Things
{
    [PublicAPI]
    public enum ThingState
    {
        None,
        Initializing,
        Initialized,
        Unknown,
        Online,
        Offline,
        Removing,
        Removed,
        Error
    }
}