using System;
using CreativeCoders.SmartHal.Kernel.Base.Items;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items.ItemTypes
{
    [UsedImplicitly]
    public class NumberItemType : ItemTypeBase<double>
    {
        public NumberItemType() : base("Number", new[] {ItemDataType.Integer, ItemDataType.Decimal})
        {
        }

        public override object ConvertValue(object value)
        {
            var numberValue = Convert.ToDouble(value);
            
            return numberValue;
        }

        public override bool ValuesAreEqual(object value0, object value1)
        {
            if (value0 == null || value1 == null)
            {
                return false;
            }

            var doubleValue0 = Convert.ToDouble(value0);
            var doubleValue1 = Convert.ToDouble(value1);

            return Math.Abs(doubleValue0 - doubleValue1) < 0.0001;
        }
    }
}