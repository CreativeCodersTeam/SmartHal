using System.Threading.Tasks;
using CreativeCoders.SmartHal.Config.Base.Items;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using CreativeCoders.SmartHal.Kernel.SubSystems.Items.Building;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items
{
    [PublicAPI]
    public class ItemSubSystem : IItemSubSystem
    {
        private readonly IItemBuilder _itemBuilder;
        
        private readonly IItemRepository _itemRepository;

        public ItemSubSystem(IItemBuilder itemBuilder, IItemRepository itemRepository)
        {
            _itemBuilder = itemBuilder;
            _itemRepository = itemRepository;
        }
        
        public Task AddItemAsync(IItemConfiguration itemConfiguration)
        {
            var item = _itemBuilder.Build(itemConfiguration);

            return _itemRepository.AddAsync(item);
        }
    }
}