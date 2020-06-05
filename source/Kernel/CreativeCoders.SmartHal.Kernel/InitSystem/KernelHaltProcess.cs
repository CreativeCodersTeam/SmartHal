using System.Threading.Tasks;
using CreativeCoders.Core.Logging;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.InitSystem
{
    [UsedImplicitly]
    public class KernelHaltProcess : IKernelHaltProcess
    {
        private static readonly ILogger Log = LogManager.GetLogger<KernelHaltProcess>();
        
        private readonly ISubSystemInitSystem _subSystemInitSystem;

        public KernelHaltProcess(ISubSystemInitSystem subSystemInitSystem)
        {
            _subSystemInitSystem = subSystemInitSystem;
        }
        
        public async Task HaltAsync()
        {
            Log.Info("Halting...");

            await _subSystemInitSystem.ExecuteHaltStepsAsync().ConfigureAwait(false);
        }
    }
}