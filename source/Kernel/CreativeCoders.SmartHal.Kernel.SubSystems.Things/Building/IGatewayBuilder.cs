using System.Threading.Tasks;
using CreativeCoders.SmartHal.Config.Base.Things;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things.Building
{
    public interface IGatewayBuilder
    {
        Task<Gateway> BuildAsync(IGatewayConfiguration gatewayConfiguration);
    }
}