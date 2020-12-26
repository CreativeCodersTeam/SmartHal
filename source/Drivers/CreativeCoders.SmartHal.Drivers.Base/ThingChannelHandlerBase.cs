using System;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Messages.Channels;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Drivers.Base
{
    [PublicAPI]
    public abstract class ThingChannelHandlerBase : IThingChannelHandler
    {
        private IDisposable _writeValueHandler;
        
        protected ThingChannelHandlerBase(ThingId thingId, string name)
        {
            Name = name;
            ChannelId = new ThingChannelId(thingId, name);
        }
        
        public Task SetupAsync(IMessageHub messageHub)
        {
            MessageHub = messageHub;

            _writeValueHandler = MessageHub
                .Handle<WriteChannelValueMessage>()
                .Where(msg => ChannelId.Equals(msg.ChannelId))
                .Register(async msg => await WriteValueAsync(msg.Value));

            return Task.CompletedTask;
        }

        public Task InitAsync()
        {
            return OnInitAsync();
        }

        protected virtual Task OnInitAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual ValueTask OnDisposeAsync()
        {
            return ValueTask.CompletedTask;
        }

        protected abstract Task WriteValueAsync(object value); 

        public string Name { get; }

        public ThingChannelId ChannelId { get; set; }
        
        public IMessageHub MessageHub { get; private set; }
        
        public ValueTask DisposeAsync()
        {
            _writeValueHandler.Dispose();
            
            return OnDisposeAsync();
        }
    }
}