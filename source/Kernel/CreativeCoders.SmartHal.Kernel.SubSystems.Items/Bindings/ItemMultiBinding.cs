using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.SmartHal.Kernel.Base.Items;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items.Bindings
{
    public class ItemMultiBinding : IItemBinding
    {
        private readonly IEnumerable<ItemBinding> _itemBindings;

        public ItemMultiBinding(IEnumerable<ItemBinding> itemBindings)
        {
            _itemBindings = itemBindings;
        }

        public Task WriteValueAsync(object value)
        {
            return _itemBindings.ForEachAsync(itemBinding => itemBinding.WriteValueAsync(value));
        }
    }
}