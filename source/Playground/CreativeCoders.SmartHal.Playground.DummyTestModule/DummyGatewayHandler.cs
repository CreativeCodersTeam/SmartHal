using System.Threading.Tasks;
using CreativeCoders.SmartHal.Drivers.Base;
using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Drivers.SetupInfos;
using CreativeCoders.SmartHal.Kernel.Base.Messages;
using CreativeCoders.SmartHal.Kernel.Base.Things;

namespace CreativeCoders.SmartHal.Playground.DummyTestModule
{
    public class DummyGatewayHandler : GatewayHandlerBase
    {
        private readonly IGatewaySetupInfo _gatewaySetupInfo;

        public DummyGatewayHandler(IGatewaySetupInfo gatewaySetupInfo)
        {
            _gatewaySetupInfo = gatewaySetupInfo;
        }

        protected override async Task<ThingState> OnInitAsync()
        {
            await Task.Delay(1000);
            
            MessageHub.SendMessage(new ThingStateChangedMessage(_gatewaySetupInfo.Id.ToString(), ThingState.Initialized));

            await Task.Delay(200);

            return ThingState.Online;
        }

        public override IThingHandler CreateThingHandler(IThingSetupInfo thingSetupInfo)
        {
            return new DummyThingHandler(thingSetupInfo);
        }
    }
}