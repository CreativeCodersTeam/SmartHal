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
    public class ItemSubSystem : IItemSubSystem
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
        
        public Task AddItemAsync(IItemConfiguration itemConfiguration)
        {
            if (_itemRepository.Any(x => x.Name == itemConfiguration.Name))
            {
                Log.Warn($"Item with name '{itemConfiguration.Name}' already exists");
                return Task.CompletedTask;
            }
            
            var item = _itemBuilder.Build(itemConfiguration);

            return _itemRepository.AddAsync(item);
        }

        public void SendCommand(string itemName, object commandValue)
        {
            _messageHub.SendMessage(new SendCommandToItemMessage(itemName,commandValue));
        }
    }
}