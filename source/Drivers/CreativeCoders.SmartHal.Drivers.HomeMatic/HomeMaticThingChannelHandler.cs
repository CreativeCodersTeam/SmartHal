using System;
using System.Threading.Tasks;
using CreativeCoders.Core.Logging;
using CreativeCoders.HomeMatic.Api.Values;
using CreativeCoders.HomeMatic.Core.Exceptions;
using CreativeCoders.HomeMatic.XmlRpc.Client;
using CreativeCoders.HomeMatic.XmlRpc.Server.Messages;
using CreativeCoders.Messaging.Core;
using CreativeCoders.SmartHal.Drivers.Base;
using CreativeCoders.SmartHal.Kernel.Base.Messages.Channels;
using CreativeCoders.SmartHal.Kernel.Base.Messages.Things;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;

namespace CreativeCoders.SmartHal.Drivers.HomeMatic
{
    public class HomeMaticThingChannelHandler : ThingChannelHandlerBase
    {
        private static readonly ILogger Log = LogManager.GetLogger<HomeMaticThingChannelHandler>();

        private readonly string _channelAddress;
        
        private readonly string _valueKey;
        
        private readonly IMediator _mediator;

        private readonly CcuValueIo _ccuValue;
        
        private IDisposable _eventHandler;

        public HomeMaticThingChannelHandler(ThingId thingId, string name, string channelAddress, string valueKey,
            IHomeMaticXmlRpcApi xmlRpcApi, IMediator mediator) : base(thingId, name)
        {
            _channelAddress = channelAddress;
            _valueKey = valueKey;
            _mediator = mediator;
            _ccuValue = new CcuValueIo(xmlRpcApi, channelAddress, valueKey);
        }

        private Task OnEvent(HomeMaticEventMessage msg)
        {
            if (msg.Address != _channelAddress || msg.ValueKey != _valueKey)
            {
                return Task.CompletedTask;
            }
            
            MessageHub.SendMessage(new ChannelHandlerValueChangedMessage(ChannelId, msg.Value));

            return Task.CompletedTask;
        }

        protected override async Task OnInitAsync()
        {
            try
            {
                var value = await _ccuValue.ReadAsync().ConfigureAwait(false);

                MessageHub.SendMessage(new ChannelHandlerValueChangedMessage(ChannelId, value));
            }
            catch (CcuXmlRpcException ex)
            {
                Log.Error("Channel handler initial value read failed", ex);

                MessageHub.SendMessage(new ThingStateChangedMessage(ChannelId, Kernel.Base.Things.ThingState.Error));
                MessageHub.SendMessage(new ThingStateChangedMessage(ChannelId.ThingId, Kernel.Base.Things.ThingState.Error));
            }
            
            _eventHandler = _mediator.RegisterAsyncHandler<HomeMaticEventMessage>(this, OnEvent);
        }

        protected override async Task WriteValueAsync(object value)
        {
            await _ccuValue.WriteAsync(value).ConfigureAwait(false);
        }

        protected override ValueTask OnDisposeAsync()
        {
            _eventHandler?.Dispose();

            return ValueTask.CompletedTask;
        }
    }
}