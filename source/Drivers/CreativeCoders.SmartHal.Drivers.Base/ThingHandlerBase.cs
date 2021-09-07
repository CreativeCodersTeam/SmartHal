using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Things;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Drivers.Base
{
    [PublicAPI]
    public abstract class ThingHandlerBase : IThingHandler
    {
        public Task SetupAsync(IMessageHub messageHub)
        {
            MessageHub = messageHub;
            
            return Task.CompletedTask;
        }
        
        public async Task<ThingState> InitAsync()
        {
            return await OnInitAsync().ConfigureAwait(false);
        }

        protected virtual Task<ThingState> OnInitAsync()
        {
            return Task.FromResult(ThingState.Online);
        }
        
        protected virtual ValueTask OnDisposeAsync()
        {
            return ValueTask.CompletedTask;
        }

        protected IMessageHub MessageHub { get; private set; }
        
        public async ValueTask DisposeAsync()
        {
            await OnDisposeAsync().ConfigureAwait(false);
        }
    }
}