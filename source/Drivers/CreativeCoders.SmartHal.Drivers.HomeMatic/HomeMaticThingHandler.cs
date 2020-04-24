using CreativeCoders.SmartHal.Drivers.Base;
using CreativeCoders.SmartHal.Kernel.Base.Drivers.SetupInfos;

namespace CreativeCoders.SmartHal.Drivers.HomeMatic
{
    public class HomeMaticThingHandler : ThingHandlerBase
    {
        private readonly IThingSetupInfo _thingSetupInfo;

        public HomeMaticThingHandler(IThingSetupInfo thingSetupInfo)
        {
            _thingSetupInfo = thingSetupInfo;
        }
    }
}