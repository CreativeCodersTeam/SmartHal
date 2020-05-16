using System.Threading.Tasks;
using CreativeCoders.SmartHal.Config.Base.Scripts;
using CreativeCoders.SmartHal.Kernel.Base.Scripting.ActionScripts;

namespace CreativeCoders.SmartHal.Kernel.Base.SubSystems
{
    public interface IScriptingSubSystem
    {
        Task AddActionScript(IActionScriptData actionScriptData);

        IActionScript FindActionScript(string scriptName);
    }
}