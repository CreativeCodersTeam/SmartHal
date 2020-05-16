using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.Core.Logging;
using CreativeCoders.SmartHal.Config.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Drivers
{
    [UsedImplicitly]
    public class DriverLoader : IDriverLoader
    {
        private static readonly ILogger Log = LogManager.GetLogger<DriverLoader>();
        
        private readonly IClassFactory _classFactory;
        
        public DriverLoader(IClassFactory classFactory)
        {
            _classFactory = classFactory;
        }

        public async Task LoadDriverAsync(IDriverConfiguration driverConfiguration, IEnumerable<DriverInfo> driverInfos, Action<DriverInstance> driverLoaded)
        {
            var driverInfo = driverInfos.FirstOrDefault(x => x.Name == driverConfiguration.Name);

            if (driverInfo == null)
            {
                Log.Warn($"Driver '{driverConfiguration.Name}' not found");

                return;
            }

            var driverInstance = _classFactory.Create(driverInfo.DriverType);

            switch (driverInstance)
            {
                case null:
                    Log.Warn($"Driver '{driverConfiguration.Name}' could not be created");
                    return;
                case IDriver driver:
                    Log.Info($"Driver '{driverConfiguration.Name} {driverInfo.Version}' loaded");
                    await driver.InitAsync().ConfigureAwait(false);
                    driverLoaded(new DriverInstance(driver, driverInfo));
                    return;
                default:
                    Log.Warn($"Driver '{driverInstance.GetType().FullName}' must implement IDriver interface");
                    return;
            }
        }

        public async Task LoadDriversAsync(IEnumerable<IDriverConfiguration> driverConfigurations, IEnumerable<DriverInfo> driverInfos, Action<DriverInstance> driverLoaded)
        {
            var driverCount = 0;
            
            await driverConfigurations
                .ForEachAsync(driverConfiguration =>
                    LoadDriverAsync(driverConfiguration, driverInfos, driver =>
                    {
                        driverLoaded(driver);
                        driverCount++;
                    })).ConfigureAwait(false);
            
            Log.Info($"{driverCount} {GetDriverText(driverCount)} loaded");
        }
        
        private static string GetDriverText(int driversCount)
        {
            return driversCount == 1
                ? "driver"
                : "drivers";
        }
    }
}