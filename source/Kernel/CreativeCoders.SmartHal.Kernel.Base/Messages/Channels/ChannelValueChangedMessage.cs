namespace CreativeCoders.SmartHal.Kernel.Base.Messages.Channels
{
    public class ChannelValueChangedMessage : SmartHalMessageBase
    {
        public ChannelValueChangedMessage(string channelId, object newValue)
        {
            ChannelId = channelId;
            NewValue = newValue;
        }
        
        public string ChannelId { get; }
        
        public object NewValue { get; }
    }
}