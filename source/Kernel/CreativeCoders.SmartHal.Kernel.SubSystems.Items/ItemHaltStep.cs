using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Halting;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items
{
    [UsedImplicitly]
    public class ItemHaltStep : IItemHaltStep
    {
        private readonly IItemRepository _itemRepository;

        public ItemHaltStep(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        
        public Task HaltAsync()
        {
            return _itemRepository.ClearAsync();
        }
    }
}