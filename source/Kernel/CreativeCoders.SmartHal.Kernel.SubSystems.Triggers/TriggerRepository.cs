using CreativeCoders.Core.Logging;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.Triggers;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Triggers
{
    [UsedImplicitly]
    public class TriggerRepository : RepositoryBase<ITrigger>, ITriggerRepository
    {
        private static readonly ILogger Log = LogManager.GetLogger<TriggerRepository>();
        
        protected override void AddItem(ITrigger item)
        {
            base.AddItem(item);
            
            Log.Info($"Trigger '{item.GetType().Name}' added");
        }

        protected override void RemoveItem(ITrigger item)
        {
            base.RemoveItem(item);
            
            Log.Info($"Trigger '{item.GetType().Name}' removed");
        }
    }
}