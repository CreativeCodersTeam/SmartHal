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
        
        private readonly IItemHaltStep _itemHaltStep;

        public KernelHaltProcess(IThingsHaltStep thingsHaltStep, IItemHaltStep itemHaltStep)
        {
            _thingsHaltStep = thingsHaltStep;
            _itemHaltStep = itemHaltStep;
        }
        
        public async Task HaltAsync()
        {
            Log.Info("Halting...");

            await _itemHaltStep.HaltAsync().ConfigureAwait(false);
            
            await _thingsHaltStep.HaltAsync().ConfigureAwait(false);
        }
    }
}