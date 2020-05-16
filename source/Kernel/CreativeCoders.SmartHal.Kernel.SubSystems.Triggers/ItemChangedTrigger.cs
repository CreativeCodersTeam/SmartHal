using System;
using System.Linq;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Messages.Items;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.Triggers;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Triggers
{
    public class ItemChangedTrigger : IItemChangedTrigger, IDisposable
    {
        private readonly IItemRepository _itemRepository;
        
        private readonly string _itemName;

        private readonly object _oldValue;
        
        private readonly object _newValue;
        
        private readonly Func<Task> _executeAsync;
        
        private readonly IDisposable _itemValueChangedHandler;

        public ItemChangedTrigger(IItemRepository itemRepository, IMessageHub messageHub, string itemName, object oldValue, object newValue, Func<Task> executeAsync)
        {
            _itemRepository = itemRepository;
            _itemName = itemName;
            _oldValue = oldValue;
            _newValue = newValue;
            _executeAsync = executeAsync;

            _itemValueChangedHandler = messageHub
                .Handle<ItemValueChangedMessage>()
                .Where(msg => msg.ItemName == itemName)
                .Register(OnItemValueChanged);
        }

        private Task OnItemValueChanged(ItemValueChangedMessage msg)
        {
            var item = _itemRepository.FirstOrDefault(x => x.Name == _itemName);

            var newValueEqual = ValuesAreEqual(_newValue, msg.NewValue,
                (value1, value2) => item?.ItemType.ValuesAreEqual(value1, value2) ?? value1 == value2);

            var oldValueEqual = ValuesAreEqual(_oldValue, msg.OldValue,
                (value1, value2) => item?.ItemType.ValuesAreEqual(value1, value2) ?? value1 == value2);

            if (!newValueEqual || !oldValueEqual)
            {
                return Task.CompletedTask;
            }

            return _executeAsync();
        }

        private static bool ValuesAreEqual(object value1, object value2, Func<object, object, bool> compare)
        {
            return compare(value1, value2);
        }

        public void Dispose()
        {
            _itemValueChangedHandler.Dispose();
        }
    }
}