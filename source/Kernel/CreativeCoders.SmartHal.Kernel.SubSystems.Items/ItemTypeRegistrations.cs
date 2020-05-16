using System.Collections.Generic;
using CreativeCoders.Core.Threading;
using CreativeCoders.SmartHal.Kernel.Base.Items;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items
{
    [UsedImplicitly]
    public class ItemTypeRegistrations : IItemTypeRegistrations
    {
        private readonly ConcurrentList<IItemType> _itemTypes;
        
        public ItemTypeRegistrations(IEnumerable<IItemType> itemTypes)
        {
            _itemTypes = new ConcurrentList<IItemType>(itemTypes);
        }
        
        public void AddItemType(IItemType itemType)
        {
            _itemTypes.Add(itemType);
        }

        public IReadOnlyCollection<IItemType> ItemTypes => _itemTypes;
    }
}