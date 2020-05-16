using System;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.Core.Logging;
using CreativeCoders.Core.Threading;
using CreativeCoders.SmartHal.Config.Base.Things;
using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Drivers.SetupInfos;
using CreativeCoders.SmartHal.Kernel.Base.Messages.Things;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Things;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things
{
    public class Gateway : IGateway, IAsyncDisposable
    {
        private static readonly ILogger Log = LogManager.GetLogger<Gateway>();
        
        private readonly IMessageHub _messageHub;
        
        private IGatewayHandler _gatewayHandler;

        private IDisposable _stateChangedHandler;

        private readonly SynchronizedValue<ThingState> _state;

        public Gateway(IGatewayConfiguration gatewayConfiguration, IMessageHub messageHub)
        {
            _messageHub = messageHub;
            Name = gatewayConfiguration.Name;
            Id = new GatewayId(gatewayConfiguration.DriverName, gatewayConfiguration.Name);
            
            _state = new SynchronizedValue<ThingState>();
        }

        internal void SetHandler(IGatewayHandler gatewayHandler)
        {
            _gatewayHandler = gatewayHandler;
        }

        internal async Task InitAsync()
        {
            _stateChangedHandler = _messageHub
                .Handle<ThingStateChangedMessage>()
                .Where(msg => msg.Id == Id.ToString() && msg.NewState != State)
                .Synchronize()
                .Register(async msg => await OnStateChanged(msg));
            
            _messageHub.SendMessage(new ThingStateChangedMessage(Id.ToString(), ThingState.Initializing));
            
            var state = await _gatewayHandler.InitAsync();
            
            _messageHub.SendMessage(new ThingStateChangedMessage(Id.ToString(), state));
        }

        private Task OnStateChanged(ThingStateChangedMessage msg)
        {
            Log.Info($"Gateway '{Id}' state changed from {State} -> {msg.NewState}");

            State = msg.NewState;
            
            return Task.CompletedTask;
        }

        public string Name { get; }

        public GatewayId Id { get; }
        
        public ThingState State
        { 
            get => _state.Value;
            private set => _state.Value = value;
        }
        
        public IThingHandler CreateThingHandler(IThingSetupInfo thingSetupInfo)
        {
            return _gatewayHandler.CreateThingHandler(thingSetupInfo);
        }

        public ValueTask DisposeAsync()
        {
            _stateChangedHandler?.Dispose();

            return _gatewayHandler.TryDisposeAsync();
        }
    }
}