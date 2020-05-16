using System.Collections.Generic;
using CreativeCoders.Core.Threading;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using CreativeCoders.SmartHal.Scripting.Base.ActionScripts.Triggers;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Scripting.ActionScripts.Triggers
{
    [UsedImplicitly]
    public class TriggerApi : ITriggerApi
    {
        private readonly ITriggerSubSystem _triggerSubSystem;
        
        private readonly IList<ITriggerBuilder> _builders;

        public TriggerApi(ITriggerSubSystem triggerSubSystem)
        {
            _triggerSubSystem = triggerSubSystem;
            _builders = new ConcurrentList<ITriggerBuilder>();
        }
        
        public IItemTriggerBuilder ForItem(string itemName)
        {
            return new ItemTriggerBuilder(itemName, builder => _builders.Add(builder), _triggerSubSystem);
        }

        public IEnumerable<ITriggerBuilder> Builders => _builders;
    }
}