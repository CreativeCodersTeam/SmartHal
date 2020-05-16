using CreativeCoders.SmartHal.Kernel.Base.Drivers;
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
        
        public string ThingId { get; }
        
        public IThingChannelHandler ThingChannelHandler { get; }
    }
}