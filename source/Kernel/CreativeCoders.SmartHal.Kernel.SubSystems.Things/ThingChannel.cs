using System;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.Core.Logging;
using CreativeCoders.Core.Threading;
using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Messages;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Things;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things
{
    public class ThingChannel : IThingChannel, IAsyncDisposable
    {
        private static readonly ILogger Log = LogManager.GetLogger<ThingChannel>();
        
        private readonly IThingChannelHandler _thingChannelHandler;
        
        private readonly IMessageHub _messageHub;
        
        private IDisposable _stateChangedHandler;
        
        private IDisposable _channelHandlerValueChangedHandler;
        
        private readonly SynchronizedValue<ThingState> _state;

        public ThingChannel(IThingChannelHandler thingChannelHandler, ThingId thingId, IMessageHub messageHub)
        {
            _thingChannelHandler = thingChannelHandler;
            _messageHub = messageHub;

            Id = new ThingChannelId(thingId, Name);
            _state = new SynchronizedValue<ThingState>();
        }

        internal Task InitAsync()
        {
            _stateChangedHandler = _messageHub
                .Handle<ThingStateChangedMessage>()
                .Where(msg => Id.Equals(msg.Id) && msg.NewState != State)
                .Synchronize()
                .Register(async msg => await OnStateChanged(msg));

            _channelHandlerValueChangedHandler = _messageHub
                .Handle<ChannelHandlerValueChangedMessage>()
                .Where(msg => Id.Equals(msg.ChannelId))
                .Synchronize()
                .Register(async msg => await OnChannelHandlerValueChanged(msg));

            return Task.CompletedTask;
        }

        private Task OnChannelHandlerValueChanged(ChannelHandlerValueChangedMessage msg)
        {
            Log.Info($"Thing Channel '{Id}' value changed from '{Value}' -> '{msg.NewValue}'");
            
            Value = msg.NewValue;
            
            return Task.CompletedTask;
        }

        private Task OnStateChanged(ThingStateChangedMessage msg)
        {
            Log.Info($"Thing channel '{Id}' state changed from {State} -> {msg.NewState}");

            State = msg.NewState;
            
            return Task.CompletedTask;
        }

        public ThingChannelId Id { get; }

        public string Name => _thingChannelHandler.Name;
        
        public ThingState State
        {
            get => _state.Value;
            private set => _state.Value = value;
        }
        
        public object Value { get; private set; }

        public ValueTask DisposeAsync()
        {
            _stateChangedHandler.Dispose();
            _channelHandlerValueChangedHandler.Dispose();
            
            return _thingChannelHandler.TryDisposeAsync();
        }
    }
}