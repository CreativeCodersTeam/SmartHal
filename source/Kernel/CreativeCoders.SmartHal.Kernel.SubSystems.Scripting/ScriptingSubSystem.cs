using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreativeCoders.Core.Logging;
using CreativeCoders.Core.Threading;
using CreativeCoders.SmartHal.Config.Base.Scripts;
using CreativeCoders.SmartHal.Kernel.Base.Scripting.ActionScripts;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using CreativeCoders.SmartHal.Scripting.Base;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Scripting
{
    [UsedImplicitly]
    public class ScriptingSubSystem : IScriptingSubSystem
    {
        private static readonly ILogger Log = LogManager.GetLogger<ScriptingSubSystem>();
        
        private readonly IScriptingCore _scriptingCore;

        private readonly IList<IActionScript> _actionScripts;

        public ScriptingSubSystem(IScriptingCore scriptingCore)
        {
            _scriptingCore = scriptingCore;
            
            _actionScripts = new ConcurrentList<IActionScript>();
        }

        public Task AddActionScript(IActionScriptData actionScriptData)
        {
            var actionScript = _scriptingCore.CreateActionScript(actionScriptData);

            if (actionScript == null)
            {
                return Task.CompletedTask;
            }
            
            _actionScripts.Add(actionScript);
                
            Log.Info($"Action script '{actionScriptData.Name}' added");

            return Task.CompletedTask;
        }

        public IActionScript FindActionScript(string actionScriptName)
        {
            return _actionScripts.FirstOrDefault(x => x.Name == actionScriptName);
        }
    }
}