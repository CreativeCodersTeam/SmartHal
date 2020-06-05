using System.Threading.Tasks;

namespace CreativeCoders.SmartHal.Kernel.InitSystem
{
    public interface IKernelBootProcess
    {
        Task BootAsync();
    }
}