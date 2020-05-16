using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Drivers.SetupInfos;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Things
{
    [PublicAPI]
    public interface IGateway
    {
        string Name { get; }

        GatewayId Id { get; }

        ThingState State { get; }
        
        IThingHandler CreateThingHandler(IThingSetupInfo thingSetupInfo);
    }
}