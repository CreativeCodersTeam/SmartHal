using CreativeCoders.SmartHal.Config.Base.Things;
using CreativeCoders.SmartHal.Kernel.Base.Drivers.SetupInfos;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things.Building.SetupInfos
{
    public class GatewaySetupInfo : SetupInfoBase, IGatewaySetupInfo
    {
        public GatewaySetupInfo(IGatewayConfiguration gatewayConfiguration, GatewayId id) : base(gatewayConfiguration.Settings)
        {
            Id = id;
            GatewayType = gatewayConfiguration.GatewayType;
            Address = gatewayConfiguration.Address;
        }
        
        public GatewayId Id { get; }
        
        public string GatewayType { get; }
        
        public string Address { get; }
    }
}