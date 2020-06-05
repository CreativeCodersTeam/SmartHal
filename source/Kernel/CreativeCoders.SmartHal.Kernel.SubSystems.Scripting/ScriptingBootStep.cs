using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.Config.Base;
using CreativeCoders.Core;
using CreativeCoders.SmartHal.Config.Base.Scripts;
using CreativeCoders.SmartHal.Kernel.Base.InitSystem;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Scripting
{
    [UsedImplicitly]
    [InitSystemStep(typeof(IScriptingSubSystem))]
    public class ScriptingBootStep : IBootStep
    {
        private readonly IScriptingSubSystem _scriptingSubSystem;
        
        private readonly IEnumerable<IActionScriptData> _actionScripts;

        public ScriptingBootStep(IScriptingSubSystem scriptingSubSystem, ISettings<IActionScriptData> actionScripts)
        {
            _scriptingSubSystem = scriptingSubSystem;
            _actionScripts = actionScripts.Values;
        }
        
        public async Task ExecuteAsync()
        {
            await _actionScripts.ForEachAsync(actionScript => _scriptingSubSystem.AddActionScript(actionScript));
        }
    }
}