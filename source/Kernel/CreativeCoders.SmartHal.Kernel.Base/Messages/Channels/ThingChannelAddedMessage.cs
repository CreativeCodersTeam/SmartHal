using CreativeCoders.SmartHal.Kernel.Base.Things;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Messages.Channels
{
    [PublicAPI]
    public class ThingChannelAddedMessage : SmartHalMessageBase
    {
        public ThingChannelAddedMessage(IThingChannel thingChannel)
        {
            ThingChannel = thingChannel;
        }
        
        public IThingChannel ThingChannel { get; }
    }
}