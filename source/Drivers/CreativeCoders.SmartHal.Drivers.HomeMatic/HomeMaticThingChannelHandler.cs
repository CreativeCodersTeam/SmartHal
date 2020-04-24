using System.Threading.Tasks;
using CreativeCoders.SmartHal.Drivers.Base;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;

namespace CreativeCoders.SmartHal.Drivers.HomeMatic
{
    public class HomeMaticThingChannelHandler : ThingChannelHandlerBase
    {
        public HomeMaticThingChannelHandler(ThingId thingId, string name) : base(thingId, name)
        {
            
        }

        protected override Task WriteValueAsync(object value)
        {
            throw new System.NotImplementedException();
        }
    }
}