using System.Linq;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.Kernel.Services.ConsoleInterface.Commands
{
    [UsedImplicitly]
    public class ExecuteActionScriptCommand : ConsoleCommandBase
    {
        private readonly IScriptingSubSystem _scriptingSubSystem;

        public ExecuteActionScriptCommand(IScriptingSubSystem scriptingSubSystem)
        {
            _scriptingSubSystem = scriptingSubSystem;
        }
        
        public override async Task ExecuteAsync(string[] arguments)
        {
            var actionScriptName = arguments.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(actionScriptName))
            {
                Output.WriteLine("No action script name specified");
                
                return;
            }

            var actionScript = _scriptingSubSystem.FindActionScript(actionScriptName);

            if (actionScript != null)
            {
                await actionScript.ExecuteAsync().ConfigureAwait(false);
            }
            
            Output.WriteLine($"Action script '{actionScriptName}' not found");
        }

        public override string CommandName => "execute-action";
    }
}