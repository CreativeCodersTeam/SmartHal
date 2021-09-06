using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;

namespace CreativeCoders.SmartHal.Kernel.Base.Messages.Channels
{
    public class ChannelHandlerValueChangedMessage : SmartHalMessageBase
    {
        public ChannelHandlerValueChangedMessage(string channelId, object newValue)
        {
            ChannelId = channelId;
            NewValue = newValue;
        }

        public ChannelHandlerValueChangedMessage(ThingChannelId thingChannelId, object newValue)
            : this(thingChannelId.ToString(), newValue) { }
        
        public string ChannelId { get; }
        
        public object NewValue { get; }
    }
}