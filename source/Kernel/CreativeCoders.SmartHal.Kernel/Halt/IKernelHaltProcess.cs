using System.Threading.Tasks;

namespace CreativeCoders.SmartHal.Kernel.Halt
{
    public interface IKernelHaltProcess
    {
        Task HaltAsync();
    }
}