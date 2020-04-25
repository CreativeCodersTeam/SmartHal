using CreativeCoders.SmartHal.Kernel.Base.Items;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items.ItemTypes
{
    [UsedImplicitly]
    public class StringItemType : ItemTypeBase<string>
    {
        public StringItemType() : base("String", new[] {ItemDataType.String})
        {
        }
    }
}