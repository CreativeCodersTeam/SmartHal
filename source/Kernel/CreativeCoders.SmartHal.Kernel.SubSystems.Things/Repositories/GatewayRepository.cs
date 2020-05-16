using CreativeCoders.Core.Logging;
using CreativeCoders.SmartHal.Kernel.Base.Messages.Gateways;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.Things;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things.Repositories
{
    [UsedImplicitly]
    public class GatewayRepository : RepositoryBase<IGateway>, IGatewayRepository
    {
        private static readonly ILogger Log = LogManager.GetLogger<GatewayRepository>();
        
        private readonly IMessageHub _messageHub;

        public GatewayRepository(IMessageHub messageHub)
        {
            _messageHub = messageHub;
        }
        
        protected override void AddItem(IGateway item)
        {
            base.AddItem(item);
            
            Log.Info($"Gateway '{item.Id}' added");
            
            _messageHub.SendMessage(new GatewayAddedMessage(item));
        }

        protected override void RemoveItem(IGateway item)
        {
            base.RemoveItem(item);
            
            Log.Info($"Gateway '{item.Id}' removed");
        }
    }
}