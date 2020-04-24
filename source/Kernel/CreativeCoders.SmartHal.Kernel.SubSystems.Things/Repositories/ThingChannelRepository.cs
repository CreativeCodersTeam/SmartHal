using CreativeCoders.Core.Logging;
using CreativeCoders.SmartHal.Kernel.Base.Messages;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.Things;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things.Repositories
{
    [UsedImplicitly]
    public class ThingChannelRepository : RepositoryBase<IThingChannel>, IThingChannelRepository
    {
        private static readonly ILogger Log = LogManager.GetLogger<ThingChannelRepository>();
        
        private readonly IMessageHub _messageHub;

        public ThingChannelRepository(IMessageHub messageHub)
        {
            _messageHub = messageHub;
        }
        
        protected override void AddItem(IThingChannel item)
        {
            base.AddItem(item);
            
            Log.Info($"Thing channel '{item.Id}' added");
            
            _messageHub.SendMessage(new ThingChannelAddedMessage(item));
        }

        protected override void RemoveItem(IThingChannel item)
        {
            base.RemoveItem(item);
            
            Log.Info($"Thing channel '{item.Id}' removed");
        }
    }
}