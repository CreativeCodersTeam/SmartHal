﻿namespace CreativeCoders.SmartHal.Kernel.Base.Messages.Channels
{
    public class ChannelHandlerValueChangedMessage : SmartHalMessageBase
    {
        public ChannelHandlerValueChangedMessage(string channelId, object newValue)
        {
            ChannelId = channelId;
            NewValue = newValue;
        }
        
        public string ChannelId { get; }
        
        public object NewValue { get; }
    }
}