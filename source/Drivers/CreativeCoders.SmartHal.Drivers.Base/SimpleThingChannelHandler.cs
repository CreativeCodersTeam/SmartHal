using System;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Messages;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Drivers.Base
{
    [PublicAPI]
    public class SimpleThingChannelHandler<T> : ThingChannelHandlerBase 
    {
        private readonly Func<T, bool> _setValue;

        public SimpleThingChannelHandler(ThingId thingId, string name, Func<T, bool> setValue) : base(thingId, name)
        {
            _setValue = setValue;
        }
        
        public SimpleThingChannelHandler(ThingId thingId, string name) : this(thingId, name, _ => true)
        {
        }

        protected override Task WriteValueAsync(object value)
        {
            var targetValue = (T) Convert.ChangeType(value, typeof(T));
            
            if (_setValue(targetValue))
            {
                SendValueUpdate(targetValue);
            }
            
            return Task.CompletedTask;
        }

        public void SendValueUpdate(T value)
        {
            Value = value;
            
            MessageHub.SendMessage(new ChannelHandlerValueChangedMessage(ChannelId.ToString(), value));
        }

        public T Value { get; set; }
    }
}