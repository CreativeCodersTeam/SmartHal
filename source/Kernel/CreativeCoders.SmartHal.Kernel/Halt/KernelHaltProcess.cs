using System.Threading.Tasks;
using CreativeCoders.Core.Logging;
using CreativeCoders.SmartHal.Kernel.Base.Halting;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Halt
{
    [UsedImplicitly]
    public class KernelHaltProcess : IKernelHaltProcess
    {
        private static readonly ILogger Log = LogManager.GetLogger<KernelHaltProcess>();
        
        private readonly IThingsHaltStep _thingsHaltStep;

        public KernelHaltProcess(IThingsHaltStep thingsHaltStep)
        {
            _thingsHaltStep = thingsHaltStep;
        }
        
        public async Task HaltAsync()
        {
            Log.Info("Halting...");

            await _thingsHaltStep.HaltAsync().ConfigureAwait(false);
        }
    }
}