using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Messages
{
    [PublicAPI]
    public class KernelStateChangedMessage : SmartHalMessageBase
    {
        public KernelStateChangedMessage(KernelState newState)
        {
            NewState = newState;
        }
        
        public KernelState NewState { get; }
    }
}