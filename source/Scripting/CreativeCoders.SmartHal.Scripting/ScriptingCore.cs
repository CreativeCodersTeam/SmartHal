using CreativeCoders.Scripting.Base;
using CreativeCoders.Scripting.Base.SourceCode;
using CreativeCoders.SmartHal.Config.Base.Scripts;
using CreativeCoders.SmartHal.Kernel.Base.Scripting.ActionScripts;
using CreativeCoders.SmartHal.Scripting.ActionScripts;
using CreativeCoders.SmartHal.Scripting.Base;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Scripting
{
    [UsedImplicitly]
    public class ScriptingCore : IScriptingCore
    {
        private readonly IActionScriptCore _actionScriptCore;

        public ScriptingCore(IActionScriptCore actionScriptCore)
        {
            _actionScriptCore = actionScriptCore;
        }
        
        public IActionScript CreateActionScript(IActionScriptData actionScriptData)
        {
            return _actionScriptCore.CreateActionScript(CreateScriptPackage(actionScriptData));
        }
        
        private static ScriptPackage CreateScriptPackage(IScriptData scriptData)
        {
            return new ScriptPackage(
                scriptData.Name,
                scriptData.Name,
                new StringSourceCode(scriptData.SourceCode)
                );
        }
    }
}