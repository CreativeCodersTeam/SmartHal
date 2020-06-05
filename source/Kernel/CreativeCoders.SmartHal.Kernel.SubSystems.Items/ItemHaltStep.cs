using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.InitSystem;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items
{
    [UsedImplicitly]
    [InitSystemStep(typeof(IItemSubSystem))]
    public class ItemHaltStep : IHaltStep
    {
        private readonly IItemRepository _itemRepository;

        public ItemHaltStep(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        
        public Task ExecuteAsync()
        {
            return _itemRepository.ClearAsync();
        }
    }
}