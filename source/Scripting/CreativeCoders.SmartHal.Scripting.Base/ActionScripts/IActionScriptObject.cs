using CreativeCoders.SmartHal.Scripting.Base.ActionScripts.Triggers;

namespace CreativeCoders.SmartHal.Scripting.Base.ActionScripts
{
    public interface IActionScriptObject
    {
        void Init();
        
        ITriggerApi Trigger { get; }
    }
}