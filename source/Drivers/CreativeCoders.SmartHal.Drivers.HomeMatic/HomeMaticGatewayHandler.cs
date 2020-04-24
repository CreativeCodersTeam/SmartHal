using System.Threading.Tasks;
using CreativeCoders.SmartHal.Drivers.Base;
using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Drivers.SetupInfos;
using CreativeCoders.SmartHal.Kernel.Base.Messages;
using CreativeCoders.SmartHal.Kernel.Base.Things;

namespace CreativeCoders.SmartHal.Drivers.HomeMatic
{
    public class HomeMaticGatewayHandler : GatewayHandlerBase
    {
        private readonly IGatewaySetupInfo _gatewaySetupInfo;

        public HomeMaticGatewayHandler(IGatewaySetupInfo gatewaySetupInfo)
        {
            _gatewaySetupInfo = gatewaySetupInfo;
        }

        protected override Task<ThingState> OnInitAsync()
        {
            MessageHub.SendMessage(new ThingStateChangedMessage(_gatewaySetupInfo.Id.ToString(), ThingState.Initialized));
            
            return Task.FromResult(ThingState.Online);
        }

        public override IThingHandler CreateThingHandler(IThingSetupInfo thingSetupInfo)
        {

            return new HomeMaticThingHandler(thingSetupInfo);
        }
    }
}