using System.Threading.Tasks;

namespace CreativeCoders.SmartHal.Kernel.Base.Scripting.ActionScripts
{
    public interface IActionScript
    {
        Task ExecuteAsync();
        
        string Name { get; }
    }
}