using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Messages.Channels
{
    [PublicAPI]
    public class NewThingChannelMessage : SmartHalMessageBase
    {
        public NewThingChannelMessage(string thingId, IThingChannelHandler thingChannelHandler)
        {
            ThingId = thingId;
            ThingChannelHandler = thingChannelHandler;
        }

        public NewThingChannelMessage(ThingId thingId, IThingChannelHandler thingChannelHandler)
            : this(thingId.ToString(), thingChannelHandler) { }


        public string ThingId { get; }
        
        public IThingChannelHandler ThingChannelHandler { get; }
    }
}