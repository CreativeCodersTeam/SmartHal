using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.Core.Logging;
using CreativeCoders.Core.Reflection;
using CreativeCoders.Core.Threading;
using CreativeCoders.SmartHal.Config.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Drivers
{
    [UsedImplicitly]
    [SubSystem("Drivers")]
    [DependsOn(typeof(IAssemblySubSystem))]
    public class DriverSubSystem : SubSystemBase, IDriverSubSystem
    {
        private static readonly ILogger Log = LogManager.GetLogger<DriverSubSystem>();

        private readonly IDriverLoader _driverLoader;

        private readonly IList<DriverInfo> _driverInfos;

        private readonly IList<DriverInstance> _driverInstances;

        public DriverSubSystem(IDriverLoader driverLoader)
        {
            _driverLoader = driverLoader;

            _driverInfos = new ConcurrentList<DriverInfo>();
            _driverInstances = new ConcurrentList<DriverInstance>();
        }

        public Task InitAsync(IEnumerable<IDriverConfiguration> driverConfigurations)
        {
            LoadDriverInfos();

            return _driverLoader.LoadDriversAsync(driverConfigurations, _driverInfos,
                driverInstance => _driverInstances.Add(driverInstance));
        }

        public IDriver GetDriver(string driverName)
        {
            return _driverInstances.FirstOrDefault(x => x.DriverInfo.Name == driverName)?.Driver;
        }

        private void LoadDriverInfos()
        {
            Log.Info("Find all drivers");

            var driverInfos =
                from type in ReflectionUtils.GetAllTypes()
                where typeof(IDriver).IsAssignableFrom(type)
                let driverAttribute = type.GetCustomAttribute<DriverAttribute>()
                where driverAttribute != null
                select driverAttribute.CreateDriverInfo(type);

            _driverInfos.AddRange(driverInfos);

            Log.Info($"{_driverInfos.Count} driver(s) found");

            _driverInfos.ForEach(driverInfo => Log.Debug($"Driver: {driverInfo.Name}"));
        }
    }
}