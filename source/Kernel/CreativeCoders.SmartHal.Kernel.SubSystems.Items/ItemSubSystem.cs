using CreativeCoders.SmartHal.Config.Base.Items;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using CreativeCoders.SmartHal.Kernel.SubSystems.Items.Building;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items
{
    [PublicAPI]
    public class ItemSubSystem : IItemSubSystem
    {
        private readonly IItemBuilder _itemBuilder;

        public ItemSubSystem(IItemBuilder itemBuilder)
        {
            _itemBuilder = itemBuilder;
        }
        
        public void AddItem(IItemConfiguration itemConfiguration)
        {
            var _ = _itemBuilder.Build(itemConfiguration);
        }
    }
}