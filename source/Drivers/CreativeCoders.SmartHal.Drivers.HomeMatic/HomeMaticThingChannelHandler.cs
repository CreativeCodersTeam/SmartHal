using System;
using System.Threading.Tasks;
using CreativeCoders.HomeMatic.Api.Values;
using CreativeCoders.HomeMatic.XmlRpc.Client;
using CreativeCoders.HomeMatic.XmlRpc.Server.Messages;
using CreativeCoders.Messaging.Core;
using CreativeCoders.SmartHal.Drivers.Base;
using CreativeCoders.SmartHal.Kernel.Base.Messages;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;

namespace CreativeCoders.SmartHal.Drivers.HomeMatic
{
    public class HomeMaticThingChannelHandler : ThingChannelHandlerBase
    {
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
            
            MessageHub.SendMessage(new ChannelHandlerValueChangedMessage(ChannelId.ToString(), msg.Value));

            return Task.CompletedTask;
        }

        protected override async Task OnInitAsync()
        {
            var value = await _ccuValue.ReadAsync();
            
            MessageHub.SendMessage(new ChannelHandlerValueChangedMessage(ChannelId.ToString(), value));
            
            _eventHandler = _mediator.RegisterAsyncHandler<HomeMaticEventMessage>(this, OnEvent);
        }

        protected override Task WriteValueAsync(object value)
        {
            return _ccuValue.WriteAsync(value);
        }

        protected override ValueTask OnDisposeAsync()
        {
            _eventHandler?.Dispose();

            return new ValueTask();
        }
    }
}