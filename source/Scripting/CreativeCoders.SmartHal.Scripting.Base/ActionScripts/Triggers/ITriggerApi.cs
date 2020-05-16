using System.Collections.Generic;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Scripting.Base.ActionScripts.Triggers
{
    [PublicAPI]
    public interface ITriggerApi
    {
        IItemTriggerBuilder ForItem(string itemName);
        
        IEnumerable<ITriggerBuilder> Builders { get; }
    }
}