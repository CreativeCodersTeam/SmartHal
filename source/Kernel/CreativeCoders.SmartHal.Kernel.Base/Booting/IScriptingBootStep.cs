using System.Threading.Tasks;

namespace CreativeCoders.SmartHal.Kernel.Base.Booting
{
    public interface IScriptingBootStep
    {
        Task InitScriptingAsync();
    }
}