using System.Collections.Generic;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Items
{
    [PublicAPI]
    public interface IItemType
    {
        string Name { get; }

        IReadOnlyCollection<ItemDataType> DataTypes{ get; }

        object ConvertValue(object value);

        bool ValuesAreEqual(object value0, object value1);
    }
}