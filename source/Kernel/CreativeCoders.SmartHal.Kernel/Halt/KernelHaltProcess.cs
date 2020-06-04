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
        
        private readonly ITriggersHaltStep _triggersHaltStep;

        private readonly IRemoteControlWebApiHaltStep _remoteControlWebApiHaltStep;

        public KernelHaltProcess(IThingsHaltStep thingsHaltStep, IItemHaltStep itemHaltStep, ITriggersHaltStep triggersHaltStep, IRemoteControlWebApiHaltStep remoteControlWebApiHaltStep)
        {
            _thingsHaltStep = thingsHaltStep;
            _itemHaltStep = itemHaltStep;
            _triggersHaltStep = triggersHaltStep;
            _remoteControlWebApiHaltStep = remoteControlWebApiHaltStep;
        }
        
        public async Task HaltAsync()
        {
            Log.Info("Halting...");

            await _remoteControlWebApiHaltStep.HaltAsync().ConfigureAwait(false);

            await _triggersHaltStep.HaltAsync().ConfigureAwait(false);

            await _itemHaltStep.HaltAsync().ConfigureAwait(false);
            
            await _thingsHaltStep.HaltAsync().ConfigureAwait(false);
        }
    }
}