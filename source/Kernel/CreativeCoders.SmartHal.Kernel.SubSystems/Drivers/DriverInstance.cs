using CreativeCoders.SmartHal.Kernel.Base.Drivers;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Drivers
{
    public class DriverInstance
    {
        public DriverInstance(IDriver driver, DriverInfo driverInfo)
        {
            Driver = driver;
            DriverInfo = driverInfo;
        }

        public IDriver Driver { get; }

        public DriverInfo DriverInfo { get; }
    }
}