using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Scripting.Base.ActionScripts.Triggers
{
    [PublicAPI]
    public interface IItemChangedTriggerBuilder
    {
        IItemChangedTriggerBuilder From(object value);

        IItemChangedTriggerBuilder To(object value);
    }
}