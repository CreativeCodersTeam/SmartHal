using System;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.Core.Logging;
using CreativeCoders.SmartHal.Kernel.Base.Scripting.ActionScripts;
using CreativeCoders.SmartHal.Scripting.Base.ActionScripts;

namespace CreativeCoders.SmartHal.Scripting.ActionScripts
{
    public class ActionScript : IActionScript
    {
        private static readonly ILogger Log = LogManager.GetLogger<ActionScript>();

        private readonly Func<Task> _executeAsync;
        
        public ActionScript(IActionScriptObject scriptObject, string name, Func<Task> executeAsync)
        {
            Name = name;
            _executeAsync = executeAsync;
            
            InitTriggers(scriptObject);
        }

        private void InitTriggers(IActionScriptObject scriptObject)
        {
            scriptObject.Init();
            
            scriptObject.Trigger.Builders.ForEach(x => x.Build(ExecuteAsync));
        }
        
        public async Task ExecuteAsync()
        {
            try
            {
                Log.Info($"Executing ActionScript '{Name}'");
                
                await _executeAsync().ConfigureAwait(false);

                Log.Info($"ActionScript '{Name}' executed");
            }
            catch (Exception e)
            {
                Log.Error("Action script execution failed.", e);
            }
        }
        
        public string Name { get; }
    }
}