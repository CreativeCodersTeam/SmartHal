using CreativeCoders.SmartHal.Kernel.Base.Things;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Messages
{
    [PublicAPI]
    public class ThingStateChangedMessage : SmartHalMessageBase
    {
        public ThingStateChangedMessage(string id, ThingState newState)
        {
            Id = id;
            NewState = newState;
        }
        
        public string Id { get; }
        
        public ThingState NewState { get; }
    }
}