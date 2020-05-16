using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CreativeCoders.SmartHal.Kernel.Base.Items;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items.ItemTypes
{
    public abstract class ItemTypeBase<T> : IItemType
    {
        protected ItemTypeBase(string name, IEnumerable<ItemDataType> dataTypes)
        {
            Name = name;
            DataTypes = dataTypes.ToArray();
        }
        
        public virtual object ConvertValue(object value)
        {
            if (value is T)
            {
                return value;
            }
            
            if (typeof(T).IsValueType && value == null)
            {
                return default(T);
            }
            
            var convertedValue = Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
            return convertedValue;
        }

        public virtual bool ValuesAreEqual(object value0, object value1)
        {
            if (ReferenceEquals(value0, value1))
            {
                return true;
            }
            
            if (value0 == null || value1 == null)
            {
                return false;
            }

            return value0.Equals(value1);
        }
        
        public string Name { get; }
        
        public IReadOnlyCollection<ItemDataType> DataTypes { get; }
    }
}