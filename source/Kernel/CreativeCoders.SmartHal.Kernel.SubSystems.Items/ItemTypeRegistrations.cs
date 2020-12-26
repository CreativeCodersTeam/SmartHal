using System.Collections.Generic;
using System.Linq;
using CreativeCoders.Core.Threading;
using CreativeCoders.SmartHal.Kernel.Base.Items;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items
{
    [UsedImplicitly]
    public class ItemTypeRegistrations : IItemTypeRegistrations
    {
        private readonly ConcurrentList<IItemType> _itemTypes;
        
        private IEnumerable<IItemType> _initItemTypes;

        public ItemTypeRegistrations(IEnumerable<IItemType> itemTypes)
        {
            _initItemTypes = itemTypes;
            _itemTypes = new ConcurrentList<IItemType>();
            //_itemTypes = new ConcurrentList<IItemType>(itemTypes);
        }
        
        public void AddItemType(IItemType itemType)
        {
            _itemTypes.Add(itemType);
        }

        public IReadOnlyCollection<IItemType> ItemTypes => _initItemTypes.Concat(_itemTypes).ToArray();// _itemTypes;
    }
}