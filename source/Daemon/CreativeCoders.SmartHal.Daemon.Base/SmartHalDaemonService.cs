using System.Threading.Tasks;
using CreativeCoders.Daemon.Base;
using CreativeCoders.Di.SimpleInjector;
using CreativeCoders.SmartHal.Config.FileSystem.Building;
using CreativeCoders.SmartHal.Kernel.Base;
using CreativeCoders.SmartHal.System;
using CreativeCoders.SmartHal.System.DefaultSystem;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using SimpleInjector;

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
            _logger.Log(LogLevel.Information, new EventId(12345), $"SmartHal worker starting. ConfigBasePath = {_smartHalDaemonConfig.ConfigBasePath}");

            var basePath = _smartHalDaemonConfig.ConfigBasePath;

            Logging.InitNlog(_smartHalDaemonConfig.LogPath);

            _kernel = new DefaultKernelBuilder()
                .UseDiContainerBuilder(() => new SimpleInjectorDiContainerBuilder(new Container()))
                .UseConfig(new FileConfigurationBuilder(basePath, true).Build())
                .Build();

            await _kernel.InitAsync().ConfigureAwait(false);

            await _kernel.StartAsync().ConfigureAwait(false);
        }

        public async Task StopAsync()
        {
            await _kernel.ShutdownAsync().ConfigureAwait(false);
        }
    }
}
