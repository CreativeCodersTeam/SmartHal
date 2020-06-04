using System.Threading.Tasks;

namespace CreativeCoders.SmartHal.Kernel.Base.Halting
{
    public interface IRemoteControlWebApiHaltStep
    {
        Task HaltAsync();
    }
}