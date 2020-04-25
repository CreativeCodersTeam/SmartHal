using System.Threading.Tasks;
using CreativeCoders.SmartHal.Drivers.Base;
using CreativeCoders.SmartHal.Kernel.Base.Drivers.SetupInfos;
using CreativeCoders.SmartHal.Kernel.Base.Messages;
using CreativeCoders.SmartHal.Kernel.Base.Things;

namespace CreativeCoders.SmartHal.Playground.DummyTestModule
{
    public class DummyThingHandler : ThingHandlerBase
    {
        private readonly IThingSetupInfo _thingSetupInfo;

        public DummyThingHandler(IThingSetupInfo thingSetupInfo)
        {
            _thingSetupInfo = thingSetupInfo;
        }

        protected override async Task<ThingState> OnInitAsync()
        {
            await Task.Delay(10000);

            MessageHub.SendMessage(new ThingStateChangedMessage(_thingSetupInfo.Id.ToString(), ThingState.Initialized));
            
            MessageHub.SendMessage(new NewThingChannelMessage(_thingSetupInfo.Id.ToString(), new DummyThingChannelHandler(_thingSetupInfo.Id, "CH_3")));
            
            MessageHub.SendMessage(new NewThingChannelMessage(_thingSetupInfo.Id.ToString(), new DummyThingChannelHandler(_thingSetupInfo.Id, "CH_4")));
            
            return ThingState.Online;
        }
    }
}