using System.Collections.Generic;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Items
{
    [PublicAPI]
    public interface IItemTypeRegistrations
    {
        void AddItemType(IItemType itemType);
        
        IReadOnlyCollection<IItemType> ItemTypes { get; }
    }
}