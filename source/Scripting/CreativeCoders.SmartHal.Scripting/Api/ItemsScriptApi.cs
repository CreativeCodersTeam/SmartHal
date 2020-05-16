using System.Linq;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using CreativeCoders.SmartHal.Scripting.Base.Api;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Scripting.Api
{
    [UsedImplicitly]
    public class ItemsScriptApi : IItemsScriptApi
    {
        private readonly IItemRepository _itemRepository;
        
        private readonly IItemSubSystem _itemSubSystem;

        public ItemsScriptApi(IItemRepository itemRepository, IItemSubSystem itemSubSystem)
        {
            _itemRepository = itemRepository;
            _itemSubSystem = itemSubSystem;
        }
        
        public IItemApi GetItem(string name)
        {
            var item = _itemRepository.FirstOrDefault(x => x.Name == name);

            return new ItemApi(item, _itemSubSystem);
        }
    }
}