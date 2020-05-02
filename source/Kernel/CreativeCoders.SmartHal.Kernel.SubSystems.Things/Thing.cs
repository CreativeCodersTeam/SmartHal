using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.Core.Logging;
using CreativeCoders.Core.Threading;
using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Messages.Channels;
using CreativeCoders.SmartHal.Kernel.Base.Messages.Things;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Things;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;
using CreativeCoders.SmartHal.Kernel.SubSystems.Things.Building;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things
{
    public class Thing : IThing, IAsyncDisposable
    {
        private static readonly ILogger Log = LogManager.GetLogger<Thing>();
        
        private readonly IThingTemplate _thingTemplate;
        
        private readonly IThingChannelBuilder _thingChannelBuilder;
        
        private readonly IMessageHub _messageHub;

        private IThingHandler _thingHandler;

        private readonly ConcurrentList<IThingChannel> _channels;
        
        private IDisposable _stateChangedHandler;
        
        private IDisposable _channelAddedHandler;
        
        private readonly SynchronizedValue<ThingState> _state;

        public Thing(string thingName, GatewayId gatewayId,
            IThingTemplate thingTemplate, IThingChannelBuilder thingChannelBuilder, IMessageHub messageHub)
        {
            _thingTemplate = thingTemplate;
            _thingChannelBuilder = thingChannelBuilder;
            _messageHub = messageHub;
            Name = thingName;
            Id = new ThingId(gatewayId, thingName);
            
            _channels = new ConcurrentList<IThingChannel>();
            _state = new SynchronizedValue<ThingState>();
        }

        internal void SetHandler(IThingHandler thingHandler)
        {
            _thingHandler = thingHandler;
        }
        
        internal async Task InitAsync()
        {
            _stateChangedHandler = _messageHub
                .Handle<ThingStateChangedMessage>()
                .Where(msg => msg.Id == Id.ToString())
                .Synchronize()
                .Register(async msg => await OnStateChanged(msg));

            _channelAddedHandler = _messageHub
                .Handle<NewThingChannelMessage>()
                .Where(msg => Id.Equals(msg.ThingId))
                .Register(async msg => await OnChannelAdded(msg));
            
            _messageHub.SendMessage(new ThingStateChangedMessage(Id.ToString(), ThingState.Initializing));
            
            var state = await _thingHandler.InitAsync();
            
            _messageHub.SendMessage(new ThingStateChangedMessage(Id.ToString(), state));
        }

        private Task OnChannelAdded(NewThingChannelMessage msg)
        {
            return AddChannel(msg.ThingChannelHandler);
        }

        private Task OnStateChanged(ThingStateChangedMessage msg)
        {
            Log.Info($"Thing '{Id}' state changed from {State} -> {msg.NewState}");

            State = msg.NewState;
            
            return Task.CompletedTask;
        }
        
        private async Task AddChannel(IThingChannelHandler thingChannelHandler)
        {
            if (!_thingTemplate.IsChannelDefined(thingChannelHandler.Name))
            {
                return;
            }
            
            Log.Info($"Channel handler '{thingChannelHandler.Name}' added");
        
            var thingChannel = await _thingChannelBuilder.Build(thingChannelHandler, this);
            
            _channels.Add(thingChannel);
        }

        public string Name { get; }

        public ThingId Id { get; }
        
        public ThingState State
        {
            get => _state.Value;
            private set => _state.Value = value;
        }

        public IReadOnlyCollection<IThingChannel> Channels => _channels;

        public ValueTask DisposeAsync()
        {
            _stateChangedHandler?.Dispose();
            _channelAddedHandler?.Dispose();

            return _thingHandler.TryDisposeAsync();
        }
    }
}