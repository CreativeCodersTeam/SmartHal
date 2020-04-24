using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Config.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Drivers;

namespace CreativeCoders.SmartHal.Kernel.Base.SubSystems
{
    public interface IDriverSubSystem
    {
        Task InitAsync(IEnumerable<IDriverConfiguration> driverConfigurations);

        IDriver GetDriver(string driverName);
    }
}