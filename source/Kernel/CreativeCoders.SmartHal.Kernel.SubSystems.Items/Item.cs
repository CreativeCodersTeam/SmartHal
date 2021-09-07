using System;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.Core.Logging;
using CreativeCoders.SmartHal.Kernel.Base.Items;
using CreativeCoders.SmartHal.Kernel.Base.Messages.Items;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items
{
    public class Item : IItem, IAsyncDisposable
    {
        private static readonly ILogger Log = LogManager.GetLogger<Item>();
        
        private readonly IItemBinding _binding;
        
        private readonly IMessageHub _messageHub;

        private readonly IDisposable _sendCommandHandler;
        
        private readonly IDisposable _itemValueUpdateHandler;

        public Item(string name, IItemType itemType, IItemBinding binding, IMessageHub messageHub)
        {
            _binding = binding;
            _messageHub = messageHub;

            Name = name;
            ItemType = itemType;

            _sendCommandHandler = messageHub
                .Handle<SendCommandToItemMessage>()
                .Where(msg => msg.ItemName == Name)
                .Register(async msg => await OnSendCommandToItem(msg));

            _itemValueUpdateHandler = messageHub
                .Handle<ItemValueUpdateMessage>()
                .Where(msg => msg.ItemName == Name)
                .Register(async msg => await OnItemValueUpdateHandler(msg));
        }

        private Task OnItemValueUpdateHandler(ItemValueUpdateMessage msg)
        {
            return UpdateValue(msg.NewValue);
        }

        private async Task OnSendCommandToItem(SendCommandToItemMessage msg)
        {
            var value = ItemType.ConvertValue(msg.CommandValue);

            await _binding.WriteValueAsync(value).ConfigureAwait(false);
        }

        private Task UpdateValue(object value)
        {
            if (ItemType.ValuesAreEqual(Value, value))
            {
                return Task.CompletedTask;
            }

            var oldValue = Value;
            
            Value = value;

            Log.Info($"Item '{Name}' value changed from '{oldValue}' to '{value}'");
            
            _messageHub.SendMessage(new ItemValueChangedMessage(Name, value, oldValue));
            
            return Task.CompletedTask;
        }

        public string Name { get; }
        
        public IItemType ItemType { get; }

        public object Value { get; private set; }
        
        public async ValueTask DisposeAsync()
        {
            _sendCommandHandler.Dispose();
            
            _itemValueUpdateHandler.Dispose();

            await _binding.TryDisposeAsync().ConfigureAwait(false);
        }
    }
}