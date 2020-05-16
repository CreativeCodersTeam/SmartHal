using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Things;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things.Building
{
    public interface IThingChannelBuilder
    {
        Task<ThingChannel> Build(IThingChannelHandler thingChannelHandler, IThing thing);
    }
}