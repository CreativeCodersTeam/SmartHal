using CreativeCoders.SmartHal.Config.Base.Scripts;
using CreativeCoders.SmartHal.Kernel.Base.Scripting.ActionScripts;

namespace CreativeCoders.SmartHal.Scripting.Base
{
    public interface IScriptingCore
    {
        IActionScript CreateActionScript(IActionScriptData actionScriptData);
    }
}