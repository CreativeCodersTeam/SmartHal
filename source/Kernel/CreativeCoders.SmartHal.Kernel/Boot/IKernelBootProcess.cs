using System.Threading.Tasks;

namespace CreativeCoders.SmartHal.Kernel.Boot
{
    public interface IKernelBootProcess
    {
        Task BootAsync();
    }
}