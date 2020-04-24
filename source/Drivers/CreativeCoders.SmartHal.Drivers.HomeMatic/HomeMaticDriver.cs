using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Drivers.SetupInfos;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Drivers.HomeMatic
{
    [UsedImplicitly]
    [Driver("HomeMatic")]
    public class HomeMaticDriver : IDriver
    {
        public Task InitAsync()
        {
            
            return Task.CompletedTask;
        }

        public IGatewayHandler CreateGatewayHandler(IGatewaySetupInfo gatewaySetupInfo)
        {

            return new HomeMaticGatewayHandler(gatewaySetupInfo);
        }
    }
}