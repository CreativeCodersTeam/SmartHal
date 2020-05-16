using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Items;
using CreativeCoders.SmartHal.Kernel.Base.Messages.Items;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items.Bindings
{
    public class InMemoryBinding : IItemBinding
    {
        private readonly string _itemName;
        
        private readonly IMessageHub _messageHub;
        
        public InMemoryBinding(string itemName, IMessageHub messageHub)
        {
            _itemName = itemName;
            _messageHub = messageHub;
        }
        
        public Task WriteValueAsync(object value)
        {
            _messageHub.SendMessage(new ItemValueUpdateMessage(_itemName, value));
            
            return Task.CompletedTask;
        }
    }
}