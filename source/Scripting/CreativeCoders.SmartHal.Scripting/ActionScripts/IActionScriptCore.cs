using CreativeCoders.Scripting.Base;
using CreativeCoders.SmartHal.Kernel.Base.Scripting.ActionScripts;

namespace CreativeCoders.SmartHal.Scripting.ActionScripts
{
    public interface IActionScriptCore
    {
        IActionScript CreateActionScript(ScriptPackage scriptPackage);
    }
}