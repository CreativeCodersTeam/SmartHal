using CreativeCoders.SmartHal.Config.Base.Things;
using CreativeCoders.SmartHal.Kernel.Base.Drivers.SetupInfos;
using CreativeCoders.SmartHal.Kernel.Base.Things;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things.Building.SetupInfos
{
    public class ThingSetupInfo : SetupInfoBase, IThingSetupInfo
    {
        public ThingSetupInfo(IThingConfiguration thingConfiguration, IThingTemplate thingTemplate, IThing thing)
            : base(thingConfiguration.Settings)
        {
            Address = thingConfiguration.Address;
            Template = thingTemplate;
            Id = thing.Id;
        }

        public ThingId Id { get; }
        
        public string Address { get; }
        
        public IThingTemplate Template { get; }
    }
}