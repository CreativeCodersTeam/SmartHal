using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.Core.Logging;
using CreativeCoders.SmartHal.Kernel.Base;
using CreativeCoders.SmartHal.Kernel.Base.Messages;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Boot;
using CreativeCoders.SmartHal.Kernel.Halt;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel
{
    [UsedImplicitly]
    public class SmartHalKernel : ISmartHalKernel
    {
        private static readonly ILogger Log = LogManager.GetLogger<SmartHalKernel>();
        
        private readonly IKernelBootProcess _bootProcess;
        
        private readonly IKernelHaltProcess _haltProcess;

        private readonly IClassFactory _classFactory;
        
        private readonly IMessageHub _messageHub;

        public SmartHalKernel(IKernelBootProcess bootProcess, IKernelHaltProcess haltProcess,
            IClassFactory classFactory, IMessageHub messageHub)
        {
            _bootProcess = bootProcess;
            _haltProcess = haltProcess;
            _classFactory = classFactory;
            _messageHub = messageHub;
        }
        
        public async Task InitAsync()
        {
            Log.Info("Initialize kernel...");
            
            await SetNewStateAsync(KernelState.Initializing).ConfigureAwait(false);
            
            await SetNewStateAsync(KernelState.Initialized).ConfigureAwait(false);
        }

        public async Task StartAsync()
        {
            Log.Info("Startup kernel...");
            
            await SetNewStateAsync(KernelState.Booting).ConfigureAwait(false);

            await _bootProcess.BootAsync().ConfigureAwait(false);
            
            await SetNewStateAsync(KernelState.Running).ConfigureAwait(false);
        }

        public async Task ShutdownAsync()
        {
            Log.Info("Shutdown kernel...");
            
            await SetNewStateAsync(KernelState.ShuttingDown).ConfigureAwait(false);

            await _haltProcess.HaltAsync().ConfigureAwait(false);

            await SetNewStateAsync(KernelState.ShutDown).ConfigureAwait(false);
        }

        public T GetService<T>()
            where T : class
        {
            return _classFactory.Create<T>();
        }

        private Task SetNewStateAsync(KernelState newState)
        {
            Log.Info($"Kernel state changed from {State} to {newState}");

            State = newState;
            
            _messageHub.SendMessage(new KernelStateChangedMessage(newState));
            
            return Task.CompletedTask;
        }

        public KernelState State { get; private set; }
    }
}