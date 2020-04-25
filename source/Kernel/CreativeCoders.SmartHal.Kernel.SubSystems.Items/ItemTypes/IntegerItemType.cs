using CreativeCoders.SmartHal.Kernel.Base.Items;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items.ItemTypes
{
    [UsedImplicitly]
    public class IntegerItemType : ItemTypeBase<int>
    {
        public IntegerItemType() : base("Integer", new []{ItemDataType.Integer, ItemDataType.Decimal}) { }
    }
}