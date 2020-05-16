using System.Threading.Tasks;
using CreativeCoders.Core.Logging;
using CreativeCoders.SmartHal.Config.Base.Things;
using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using CreativeCoders.SmartHal.Kernel.Base.Things;
using CreativeCoders.SmartHal.Kernel.SubSystems.Things.Building.SetupInfos;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things.Building
{
    [UsedImplicitly]
    public class GatewayBuilder : IGatewayBuilder
    {
        private static readonly ILogger Log = LogManager.GetLogger<GatewayBuilder>();
        
        private readonly IDriverSubSystem _driverSubSystem;
        
        private readonly IGatewayRepository _gatewayRepository;
        
        private readonly IMessageHub _messageHub;

        public GatewayBuilder(IDriverSubSystem driverSubSystem, IGatewayRepository gatewayRepository, IMessageHub messageHub)
        {
            _driverSubSystem = driverSubSystem;
            _gatewayRepository = gatewayRepository;
            _messageHub = messageHub;
        }
        
        public async Task<Gateway> Build(IGatewayConfiguration gatewayConfiguration)
        {
            var gateway = new Gateway(gatewayConfiguration, _messageHub);
            
            var gatewayHandler = CreateGatewayHandler(gatewayConfiguration, gateway);

            if (gatewayHandler == null)
            {
                Log.Error($"Driver '{gatewayConfiguration.DriverName}' does not create gateway handler for gateway '{gatewayConfiguration.Name}'");
                
                return null;
            }
            
            Log.Info($"Gateway handler for gateway '{gatewayConfiguration.Name}' created");
            
            gateway.SetHandler(gatewayHandler);

            await gatewayHandler.SetupAsync(_messageHub);
            
            await _gatewayRepository.AddAsync(gateway).ConfigureAwait(false);
            
            return gateway;
        }

        private IGatewayHandler CreateGatewayHandler(IGatewayConfiguration gatewayConfiguration, IGateway gateway)
        {
            var driver = _driverSubSystem.GetDriver(gatewayConfiguration.DriverName);

            if (driver == null)
            {
                Log.Warn($"Driver '{gatewayConfiguration.DriverName}' for gateway '{gatewayConfiguration.Name}' not found");

                return null;
            }

            var gatewayHandler = driver.CreateGatewayHandler(new GatewaySetupInfo(gatewayConfiguration, gateway.Id));
            
            return gatewayHandler;
        }
    }
}