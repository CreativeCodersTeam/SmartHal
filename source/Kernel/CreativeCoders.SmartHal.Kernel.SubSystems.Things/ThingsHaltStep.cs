using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Halting;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things
{
    [UsedImplicitly]
    public class ThingsHaltStep : IThingsHaltStep
    {
        private readonly IGatewayRepository _gatewayRepository;
        
        private readonly IThingRepository _thingRepository;
        
        private readonly IThingChannelRepository _thingChannelRepository;

        public ThingsHaltStep(IGatewayRepository gatewayRepository, IThingRepository thingRepository, IThingChannelRepository thingChannelRepository)
        {
            _gatewayRepository = gatewayRepository;
            _thingRepository = thingRepository;
            _thingChannelRepository = thingChannelRepository;
        }
        
        public async Task HaltAsync()
        {
            await _thingChannelRepository.ClearAsync().ConfigureAwait(false);
            await _thingRepository.ClearAsync().ConfigureAwait(false);
            await _gatewayRepository.ClearAsync().ConfigureAwait(false);
        }
    }
}