using System;
using CreativeCoders.SmartHal.Kernel.Base.Items;
using CreativeCoders.SmartHal.Kernel.Base.Items.DataTypes;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items.ItemTypes
{
    [UsedImplicitly]
    public class DimmerItemType : ItemTypeBase<double>
    {
        public DimmerItemType() : base("Dimmer",
            new[] {ItemDataType.Switch, ItemDataType.Percent, ItemDataType.Decimal})
        {
        }

        public override object ConvertValue(object value)
        {
            if (value is not SwitchValue switchValue)
            {
                return base.ConvertValue(value);
            }

            double doubleValue = switchValue;
            return doubleValue;

        }

        public override bool ValuesAreEqual(object value0, object value1)
        {
            if (value0 == null || value1 == null)
            {
                return false;
            }

            if (value0 == value1)
            {
                return true;
            }

            var doubleValue0 = (double) ConvertValue(value0);
            var doubleValue1 = (double) ConvertValue(value1);

            return Math.Abs(doubleValue0 - doubleValue1) < 0.0001;
        }
    }
}