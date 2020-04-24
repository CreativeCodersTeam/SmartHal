namespace CreativeCoders.SmartHal.Kernel.Base.Messages
{
    public class WriteChannelValueMessage : SmartHalMessageBase
    {
        public WriteChannelValueMessage(string channelId, object value)
        {
            ChannelId = channelId;
            Value = value;
        }

        public string ChannelId { get; }
        
        public object Value { get; }
    }
}