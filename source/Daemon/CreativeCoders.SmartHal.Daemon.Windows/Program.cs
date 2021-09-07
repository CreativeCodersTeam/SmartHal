using System;
using System.Linq;
using System.Threading.Tasks;
using CreativeCoders.Core.IO;
using CreativeCoders.Core.SysEnvironment;
using CreativeCoders.Daemon.Windows;
using CreativeCoders.SmartHal.Daemon.Base;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace CreativeCoders.SmartHal.Daemon.Windows
{
    [UsedImplicitly]
    public class Program
    {
        private const string SmartHalConfigFileName = "smarthal.json";

        [UsedImplicitly]
        public static async Task Main(string[] args)
        {
            var daemonConfig = CreateDaemonConfig(args);

            if (!FileSys.Directory.Exists(daemonConfig.ConfigBasePath))
            {
                throw new ArgumentException("No config base path given");
            }

            await new WindowsServiceDaemon<SmartHalDaemonService, SmartHalDaemonConfig>(daemonConfig)
                .RunAsync(args)
                .ConfigureAwait(false);
        }

        private static SmartHalDaemonConfig CreateDaemonConfig(string[] args)
        {
            var configFileName = FileSys.Path.Combine(Env.GetAppDirectory(), SmartHalConfigFileName);

            var daemonConfig = FileSys.File.Exists(configFileName)
                ? JsonConvert.DeserializeObject<SmartHalDaemonConfig>(FileSys.File.ReadAllText(configFileName))
                : new SmartHalDaemonConfig();

            var configPathArg = ReadConfigBasePath(args);

            if (!string.IsNullOrWhiteSpace(configPathArg))
            {
                daemonConfig.ConfigBasePath = configPathArg;
            }

            return daemonConfig;
        }

        private static string ReadConfigBasePath(string[] args)
        {
            var arg = args.FirstOrDefault(
                x => x.StartsWith("configBasePath=", StringComparison.CurrentCultureIgnoreCase));

            var separatorIndex = arg?.IndexOf("=", StringComparison.CurrentCulture);

            return separatorIndex == null
                ? null
                : arg[(separatorIndex.Value + 1)..];
        }
    }
}
