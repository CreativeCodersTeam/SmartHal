using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.Things;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things.Building
{
    [UsedImplicitly]
    public class ThingChannelBuilder : IThingChannelBuilder
    {
        private readonly IThingChannelRepository _thingChannelRepository;
        
        private readonly IMessageHub _messageHub;

        public ThingChannelBuilder(IThingChannelRepository thingChannelRepository, IMessageHub messageHub)
        {
            _thingChannelRepository = thingChannelRepository;
            _messageHub = messageHub;
        }
        
        public async Task<ThingChannel> Build(IThingChannelHandler thingChannelHandler, IThing thing)
        {
            var thingChannel = new ThingChannel(thingChannelHandler, thing.Id, _messageHub);
            
            await thingChannelHandler.SetupAsync(_messageHub).ConfigureAwait(false);
            
            await _thingChannelRepository.AddAsync(thingChannel).ConfigureAwait(false);
            
            await thingChannel.InitAsync().ConfigureAwait(false);

            await thingChannelHandler.InitAsync().ConfigureAwait(false);

            return thingChannel;
        }
    }
}