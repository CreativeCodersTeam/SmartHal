using System.Threading.Tasks;
using CreativeCoders.Core.Logging;
using CreativeCoders.SmartHal.Kernel.Base.Booting;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Boot
{
    [UsedImplicitly]
    public class KernelBootProcess : IKernelBootProcess
    {
        private static readonly ILogger Log = LogManager.GetLogger<KernelBootProcess>();
        
        private readonly IAssemblyBootStep _assemblyBootStep;
        
        private readonly IDriverBootStep _driverBootStep;
        
        private readonly IThingsBootStep _thingsBootStep;
        
        private readonly IItemBootStep _itemBootStep;

        public KernelBootProcess(IAssemblyBootStep assemblyBootStep, IDriverBootStep driverBootStep,
            IThingsBootStep thingsBootStep, IItemBootStep itemBootStep)
        {
            _assemblyBootStep = assemblyBootStep;
            _driverBootStep = driverBootStep;
            _thingsBootStep = thingsBootStep;
            _itemBootStep = itemBootStep;
        }

        public async Task BootAsync()
        {
            Log.Info("Booting...");
            
            await _assemblyBootStep.LoadAssembliesAsync().ConfigureAwait(false);

            await _driverBootStep.LoadDriversAsync().ConfigureAwait(false);

            await _thingsBootStep.InitThingsAsync().ConfigureAwait(false);

            await _itemBootStep.InitItemsAsync().ConfigureAwait(false);
        }
    }
}