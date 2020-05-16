using System;
using System.Threading.Tasks;
using CreativeCoders.Net.Avm;
using CreativeCoders.SmartHal.Drivers.Base;
using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Drivers.SetupInfos;
using CreativeCoders.SmartHal.Kernel.Base.Things;

namespace CreativeCoders.SmartHal.Drivers.AvmFritzBox
{
    public class FritzBoxGatewayHandler : GatewayHandlerBase
    {
        private readonly IGatewaySetupInfo _gatewaySetupInfo;
        
        private FritzBox _fritzBox;

        public FritzBoxGatewayHandler(IGatewaySetupInfo gatewaySetupInfo)
        {
            _gatewaySetupInfo = gatewaySetupInfo;
        }

        protected override Task<ThingState> OnInitAsync()
        {
            var url = _gatewaySetupInfo.Address;
            var userName = _gatewaySetupInfo.ReadSetting<string>("UserName");
            var password = _gatewaySetupInfo.ReadSetting<string>("Password");
            
            _fritzBox = new FritzBox(url, userName, password);
            
            return Task.FromResult(ThingState.Online);
        }

        public override IThingHandler CreateThingHandler(IThingSetupInfo thingSetupInfo)
        {
            var thingType = thingSetupInfo.ReadSetting<string>("Type");
            
            return thingType?.Equals("WlanDevice", StringComparison.InvariantCultureIgnoreCase) == true
                ? new FritzBoxWlanDeviceThingHandler(thingSetupInfo, _fritzBox)
                : null;
        }
    }
}