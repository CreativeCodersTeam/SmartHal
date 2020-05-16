using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Scripting.Base.ActionScripts.Triggers
{
    [PublicAPI]
    public interface IItemTriggerBuilder
    {
        IItemChangedTriggerBuilder Changed();
    }
}