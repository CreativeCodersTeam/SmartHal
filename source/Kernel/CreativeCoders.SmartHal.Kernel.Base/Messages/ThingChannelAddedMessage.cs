using CreativeCoders.SmartHal.Kernel.Base.Things;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Messages
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