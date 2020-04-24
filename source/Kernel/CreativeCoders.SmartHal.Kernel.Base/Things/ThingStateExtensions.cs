namespace CreativeCoders.SmartHal.Kernel.Base.Things
{
    public static class ThingStateExtensions
    {
        public static bool IsInitialized(this ThingState state)
        {
            return (int) state >= (int) ThingState.Initialized;
        }
    }
}