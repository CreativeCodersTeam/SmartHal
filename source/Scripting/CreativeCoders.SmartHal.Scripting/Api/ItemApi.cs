using CreativeCoders.SmartHal.Kernel.Base.Items;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using CreativeCoders.SmartHal.Scripting.Base.Api;

namespace CreativeCoders.SmartHal.Scripting.Api
{
    public class ItemApi : IItemApi
    {
        private readonly IItem _item;
        
        private readonly IItemSubSystem _itemSubSystem;

        public ItemApi(IItem item, IItemSubSystem itemSubSystem)
        {
            _item = item;
            _itemSubSystem = itemSubSystem;
        }

        public object Value => _item.Value;
        
        public void SendCommand(object commandValue)
        {
            _itemSubSystem.SendCommand(_item.Name, commandValue);
        }
    }
}