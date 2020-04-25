using CreativeCoders.Core.Logging;
using CreativeCoders.SmartHal.Kernel.Base.Items;
using CreativeCoders.SmartHal.Kernel.Base.Messages;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Items
{
    [UsedImplicitly]
    public class ItemRepository : RepositoryBase<IItem>, IItemRepository
    {
        private static readonly ILogger Log = LogManager.GetLogger<ItemRepository>();
        
        private readonly IMessageHub _messageHub;

        public ItemRepository(IMessageHub messageHub)
        {
            _messageHub = messageHub;
        }
        
        protected override void AddItem(IItem item)
        {
            base.AddItem(item);
            
            Log.Info($"Item '{item.Name}' added");
            
            _messageHub.SendMessage(new ItemAddedMessage(item));
        }

        protected override void RemoveItem(IItem item)
        {
            base.RemoveItem(item);
            
            Log.Info($"Item '{item.Name}' removed");
        }
    }
}