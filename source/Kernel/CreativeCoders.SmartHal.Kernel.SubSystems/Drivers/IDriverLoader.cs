using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Config.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Drivers
{
    [PublicAPI]
    public interface IDriverLoader
    {
        Task LoadDriverAsync(IDriverConfiguration driverConfiguration, IEnumerable<DriverInfo> driverInfos,
            Action<DriverInstance> driverLoaded);
        
        Task LoadDriversAsync(IEnumerable<IDriverConfiguration> driverConfigurations,
            IEnumerable<DriverInfo> driverInfos, Action<DriverInstance> driverLoaded);
    }
}