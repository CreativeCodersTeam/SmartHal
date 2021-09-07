using System.Linq;
using System.Threading.Tasks;
using CreativeCoders.Core.Logging;
using CreativeCoders.SmartHal.Config.Base.Items;
using CreativeCoders.SmartHal.Kernel.Base.Messages.Items;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using CreativeCoders.SmartHal.Kernel.SubSystems.Items.Building;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items
{
    [PublicAPI]
    [SubSystem("Items")]
    [DependsOn(typeof(IThingSubSystem))]
    public class ItemSubSystem : SubSystemBase, IItemSubSystem
    {
        private static readonly ILogger Log = LogManager.GetLogger<ItemSubSystem>();
        
        private readonly IItemBuilder _itemBuilder;
        
        private readonly IItemRepository _itemRepository;
        
        private readonly IMessageHub _messageHub;

        public ItemSubSystem(IItemBuilder itemBuilder, IItemRepository itemRepository, IMessageHub messageHub)
        {
            _itemBuilder = itemBuilder;
            _itemRepository = itemRepository;
            _messageHub = messageHub;
        }
        
        public async Task AddItemAsync(IItemConfiguration itemConfiguration)
        {
            if (_itemRepository.Any(x => x.Name == itemConfiguration.Name))
            {
                Log.Warn($"Item with name '{itemConfiguration.Name}' already exists");
                return;
            }
            
            var item = _itemBuilder.Build(itemConfiguration);

            await _itemRepository.AddAsync(item).ConfigureAwait(false);
        }

        public void SendCommand(string itemName, object commandValue)
        {
            _messageHub.SendMessage(new SendCommandToItemMessage(itemName,commandValue));
        }
    }
}