using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.Config.Base;
using CreativeCoders.Core;
using CreativeCoders.SmartHal.Config.Base.Scripts;
using CreativeCoders.SmartHal.Kernel.Base.Booting;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Scripting
{
    [UsedImplicitly]
    public class ScriptingBootStep : IScriptingBootStep
    {
        private readonly IScriptingSubSystem _scriptingSubSystem;
        
        private readonly IEnumerable<IActionScriptData> _actionScripts;

        public ScriptingBootStep(IScriptingSubSystem scriptingSubSystem, ISettings<IActionScriptData> actionScripts)
        {
            _scriptingSubSystem = scriptingSubSystem;
            _actionScripts = actionScripts.Values;
        }
        
        public async Task InitScriptingAsync()
        {
            await _actionScripts.ForEachAsync(actionScript => _scriptingSubSystem.AddActionScript(actionScript));
        }
    }
}