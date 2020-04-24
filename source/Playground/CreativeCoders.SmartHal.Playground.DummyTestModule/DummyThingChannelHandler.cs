using System.Threading.Tasks;
using CreativeCoders.SmartHal.Drivers.Base;
using CreativeCoders.SmartHal.Kernel.Base.Messages;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;

namespace CreativeCoders.SmartHal.Playground.DummyTestModule
{
    public class DummyThingChannelHandler : ThingChannelHandlerBase
    {
        public DummyThingChannelHandler(ThingId thingId, string name) : base(thingId, name)
        {
        }

        protected override Task OnInitAsync()
        {
            MessageHub.SendMessage(new ChannelHandlerValueChangedMessage(ChannelId.ToString(), 100));
            
            return Task.CompletedTask;
        }

        protected override Task WriteValueAsync(object value)
        {
            MessageHub.SendMessage(new ChannelHandlerValueChangedMessage(ChannelId.ToString(), value));
            
            return Task.CompletedTask;
        }
    }
}