using System;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Items;
using CreativeCoders.SmartHal.Kernel.Base.Messages;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items
{
    public class Item : IItem, IAsyncDisposable
    {
        private readonly IItemBinding _binding;
        
        private readonly IDisposable _sendCommandHandler;

        public Item(string name, IItemType itemType, IItemBinding binding, IMessageHub messageHub)
        {
            _binding = binding;
            
            Name = name;
            ItemType = itemType;

            _sendCommandHandler = messageHub
                .Handle<SendCommandToItemMessage>()
                .Where(msg => msg.ItemName == Name)
                .Register(async msg => await OnSendCommandToItem(msg));
        }

        private Task OnSendCommandToItem(SendCommandToItemMessage msg)
        {
            var value = ItemType.ConvertValue(msg.CommandValue);

            return _binding.WriteValueAsync(value);
        }

        public string Name { get; }
        
        public IItemType ItemType { get; }
        
        public ValueTask DisposeAsync()
        {
            _sendCommandHandler.Dispose();
            
            return new ValueTask();
        }
    }
}