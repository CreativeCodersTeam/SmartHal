using System.Threading.Tasks;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base
{
    [PublicAPI]
    public interface ISmartHalKernel
    {
        Task InitAsync();

        Task StartAsync();

        Task ShutdownAsync();

        T GetService<T>()
            where T : class;
        
        KernelState State { get; }
    }
}