using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Drivers.SetupInfos;

namespace CreativeCoders.SmartHal.Kernel.Base.Drivers
{
    public interface IDriver
    {
        Task InitAsync();
        
        IGatewayHandler CreateGatewayHandler(IGatewaySetupInfo gatewaySetupInfo);
    }
}