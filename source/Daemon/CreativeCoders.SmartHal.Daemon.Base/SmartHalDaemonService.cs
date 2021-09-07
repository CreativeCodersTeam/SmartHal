using System.Threading.Tasks;
using CreativeCoders.Daemon.Base;
using CreativeCoders.SmartHal.Kernel.Base;
using CreativeCoders.SmartHal.System.Boot;
using CreativeCoders.SmartHal.System.DefaultSystem;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace CreativeCoders.SmartHal.Daemon.Base
{
    [UsedImplicitly]
    public class SmartHalDaemonService : IDaemonService
    {
        private readonly ILogger<SmartHalDaemonService> _logger;

        private readonly SmartHalDaemonConfig _smartHalDaemonConfig;

        private ISmartHalKernel _kernel;

        public SmartHalDaemonService(ILogger<SmartHalDaemonService> logger, SmartHalDaemonConfig smartHalDaemonConfig)
        {
            _logger = logger;
            _smartHalDaemonConfig = smartHalDaemonConfig;
        }

        public async Task StartAsync()
        {
            const string logMessage = "SmartHal worker starting. ConfigBasePath = {ConfigBasePath}";

            _logger.Log(LogLevel.Information, new EventId(12345), logMessage, _smartHalDaemonConfig.ConfigBasePath);

            var basePath = _smartHalDaemonConfig.ConfigBasePath;

            Logging.InitNlog(_smartHalDaemonConfig.LogPath);

            _kernel = await new BootLoader<DefaultKernelBuilder>()
                .SetInstancePath(basePath)
                .StartKernelAsync()
                .ConfigureAwait(false);
        }

        public async Task StopAsync()
        {
            _logger.Log(LogLevel.Information, new EventId(12346), "SmartHal worker stopping.");

            await _kernel.ShutdownAsync().ConfigureAwait(false);
        }
    }
}
