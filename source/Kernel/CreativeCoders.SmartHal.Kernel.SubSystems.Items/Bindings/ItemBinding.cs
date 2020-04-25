using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Items;
using CreativeCoders.SmartHal.Kernel.Base.Messages;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items.Bindings
{
    public class ItemBinding : IItemBinding
    {
        private readonly string _channelId;
        
        private readonly IMessageHub _messageHub;

        public ItemBinding(string channelId, IMessageHub messageHub)
        {
            _channelId = channelId;
            _messageHub = messageHub;
        }

        public Task WriteValueAsync(object value)
        {
            _messageHub.SendMessage(new WriteChannelValueMessage(_channelId, value));
            
            return Task.CompletedTask;
        }
    }
}