using CreativeCoders.SmartHal.Kernel.Base.Things;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Messages.Things
{
    [PublicAPI]
    public class ThingStateChangedMessage : SmartHalMessageBase
    {
        public ThingStateChangedMessage(string id, ThingState newState)
        {
            Id = id;
            NewState = newState;
        }

        public ThingStateChangedMessage(GatewayId gatewayId, ThingState thingState)
            : this(gatewayId.ToString(), thingState) { }

        public ThingStateChangedMessage(ThingId thingId, ThingState thingState)
            : this(thingId.ToString(), thingState) {  }

        public ThingStateChangedMessage(ThingChannelId thingChannelId, ThingState thingState)
            : this(thingChannelId.ToString(), thingState) { }

        public string Id { get; }
        
        public ThingState NewState { get; }
    }
}