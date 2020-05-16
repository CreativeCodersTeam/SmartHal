using CreativeCoders.SmartHal.Kernel.Base.Items;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items.ItemTypes
{
    [UsedImplicitly]
    public class BoolItemType : ItemTypeBase<bool>
    {
        public BoolItemType()
            : base("Bool", new[]
            {
                ItemDataType.Bool, ItemDataType.Integer, ItemDataType.Switch, ItemDataType.OpenClosed,
                ItemDataType.Decimal
            })
        {
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

            var boolValue0 = (bool) ConvertValue(value0);
            var boolValue1 = (bool) ConvertValue(value1);

            return boolValue0 == boolValue1;
        }
    }
}