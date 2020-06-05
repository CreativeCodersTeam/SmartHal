using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.Config.Base;
using CreativeCoders.SmartHal.Config.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.InitSystem;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Drivers
{
    [UsedImplicitly]
    [InitSystemStep(typeof(IDriverSubSystem))]
    public class DriverBootStep : IBootStep
    {
        private readonly IDriverSubSystem _driverSubSystem;
        
        private readonly IEnumerable<IDriverConfiguration> _driverConfigurations;

        public DriverBootStep(ISettings<IDriverConfiguration> driverConfigurations, IDriverSubSystem driverSubSystem)
        {
            _driverConfigurations = driverConfigurations.Values;
            _driverSubSystem = driverSubSystem;
        }
        
        public Task ExecuteAsync()
        {
            return _driverSubSystem.InitAsync(_driverConfigurations);
        }
    }
}