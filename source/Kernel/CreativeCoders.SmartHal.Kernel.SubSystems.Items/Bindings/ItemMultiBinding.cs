using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.SmartHal.Kernel.Base.Items;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items.Bindings
{
    public class ItemMultiBinding : IItemBinding, IAsyncDisposable
    {
        private readonly IEnumerable<ItemBinding> _itemBindings;

        public ItemMultiBinding(IEnumerable<ItemBinding> itemBindings)
        {
            _itemBindings = itemBindings;
        }

        public async Task WriteValueAsync(object value)
        {
            await _itemBindings
                .ForEachAsync(
                    async itemBinding => await itemBinding.WriteValueAsync(value).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        public async ValueTask DisposeAsync()
        {
            await _itemBindings
                .ForEachAsync(
                    async itemBinding => await itemBinding.TryDisposeAsync().ConfigureAwait(false))
                .ConfigureAwait(false);
        }
    }
}