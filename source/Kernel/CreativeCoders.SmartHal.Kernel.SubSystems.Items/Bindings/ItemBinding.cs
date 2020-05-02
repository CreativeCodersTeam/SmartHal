using System;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Items;
using CreativeCoders.SmartHal.Kernel.Base.Messages.Channels;
using CreativeCoders.SmartHal.Kernel.Base.Messages.Items;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items.Bindings
{
    public class ItemBinding : IItemBinding, IAsyncDisposable
    {
        private readonly string _itemName;
        
        private readonly string _channelId;
        
        private readonly IMessageHub _messageHub;
        
        private readonly IDisposable _channelValueChangedHandler;

        public ItemBinding(string itemName, string channelId, IMessageHub messageHub)
        {
            _itemName = itemName;
            _channelId = channelId;
            _messageHub = messageHub;

            _channelValueChangedHandler = _messageHub
                .Handle<ChannelValueChangedMessage>()
                .Where(msg => msg.ChannelId == channelId)
                .Register(async msg => await OnChannelValueChanged(msg));
        }

        private Task OnChannelValueChanged(ChannelValueChangedMessage msg)
        {
            _messageHub.SendMessage(new ItemValueUpdateMessage(_itemName, msg.NewValue));
            
            return Task.CompletedTask;
        }

        public Task WriteValueAsync(object value)
        {
            _messageHub.SendMessage(new WriteChannelValueMessage(_channelId, value));
            
            return Task.CompletedTask;
        }

        public ValueTask DisposeAsync()
        {
            _channelValueChangedHandler.Dispose();

            return new ValueTask();
        }
    }
}