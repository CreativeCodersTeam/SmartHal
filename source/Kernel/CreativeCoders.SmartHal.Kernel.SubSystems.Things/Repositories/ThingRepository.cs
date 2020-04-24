using CreativeCoders.Core.Logging;
using CreativeCoders.SmartHal.Kernel.Base.Messages;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.Things;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things.Repositories
{
    [UsedImplicitly]
    public class ThingRepository : RepositoryBase<IThing>, IThingRepository
    {
        private static readonly ILogger Log = LogManager.GetLogger<ThingRepository>();
        
        private readonly IMessageHub _messageHub;

        public ThingRepository(IMessageHub messageHub)
        {
            _messageHub = messageHub;
        }
        
        protected override void AddItem(IThing item)
        {
            base.AddItem(item);
            
            Log.Info($"Thing '{item.Id}' added");
            
            _messageHub.SendMessage(new ThingAddedMessage(item));
        }

        protected override void RemoveItem(IThing item)
        {
            base.RemoveItem(item);
            
            Log.Info($"Thing '{item.Id}' removed");
        }
    }
}