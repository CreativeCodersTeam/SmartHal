using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;

namespace CreativeCoders.SmartHal.Kernel.Base.Messages.Channels
{
    public class ChannelValueChangedMessage : SmartHalMessageBase
    {
        public ChannelValueChangedMessage(string channelId, object newValue)
        {
            ChannelId = channelId;
            NewValue = newValue;
        }

        public ChannelValueChangedMessage(ThingChannelId thingChannelId, object newValue)
            : this(thingChannelId.ToString(), newValue) { }
        
        public string ChannelId { get; }
        
        public object NewValue { get; }
    }
}