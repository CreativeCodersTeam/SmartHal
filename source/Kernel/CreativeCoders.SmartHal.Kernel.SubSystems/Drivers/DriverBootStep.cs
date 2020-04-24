using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.Config.Base;
using CreativeCoders.SmartHal.Config.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Booting;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Drivers
{
    [UsedImplicitly]
    public class DriverBootStep : IDriverBootStep
    {
        private readonly IDriverSubSystem _driverSubSystem;
        
        private readonly IEnumerable<IDriverConfiguration> _driverConfigurations;

        public DriverBootStep(ISettings<IDriverConfiguration> driverConfigurations, IDriverSubSystem driverSubSystem)
        {
            _driverConfigurations = driverConfigurations.Values;
            _driverSubSystem = driverSubSystem;
        }
        
        public Task LoadDriversAsync()
        {
            return _driverSubSystem.InitAsync(_driverConfigurations);
        }
    }
}