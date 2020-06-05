using System.Threading.Tasks;
using CreativeCoders.Core.Logging;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.InitSystem
{
    [UsedImplicitly]
    public class KernelBootProcess : IKernelBootProcess
    {
        private static readonly ILogger Log = LogManager.GetLogger<KernelBootProcess>();

        private readonly ISubSystemInitSystem _subSystemInitSystem;

        public KernelBootProcess(ISubSystemInitSystem subSystemInitSystem)
        {
            _subSystemInitSystem = subSystemInitSystem;
        }

        public async Task BootAsync()
        {
            Log.Info("Booting...");

            await _subSystemInitSystem.ExecuteBootStepsAsync().ConfigureAwait(false);
        }
    }
}