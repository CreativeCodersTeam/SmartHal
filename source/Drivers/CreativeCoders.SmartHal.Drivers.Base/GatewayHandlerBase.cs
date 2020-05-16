using System;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Drivers.SetupInfos;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Things;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Drivers.Base
{
    [PublicAPI]
    public abstract class GatewayHandlerBase : IGatewayHandler, IAsyncDisposable
    {
        public Task SetupAsync(IMessageHub messageHub)
        {
            MessageHub = messageHub;

            return OnSetupAsync();
        }

        protected virtual Task OnSetupAsync()
        {
            return Task.CompletedTask;
        }

        public Task<ThingState> InitAsync()
        {
            return OnInitAsync();
        }

        protected virtual Task<ThingState> OnInitAsync()
        {
            return Task.FromResult(ThingState.Online);
        }

        protected virtual ValueTask OnDisposeAsync()
        {
            return new ValueTask();
        }

        protected IMessageHub MessageHub { get; private set; }

        public abstract IThingHandler CreateThingHandler(IThingSetupInfo thingSetupInfo);
        
        public ValueTask DisposeAsync()
        {
            return OnDisposeAsync();
        }
    }
}