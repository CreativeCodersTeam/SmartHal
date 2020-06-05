using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.InitSystem;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things
{
    [UsedImplicitly]
    [InitSystemStep(typeof(IThingSubSystem))]
    public class ThingsHaltStep : IHaltStep
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
        
        public async Task ExecuteAsync()
        {
            await _thingChannelRepository.ClearAsync().ConfigureAwait(false);

            await _thingRepository.ClearAsync().ConfigureAwait(false);

            await _gatewayRepository.ClearAsync().ConfigureAwait(false);
        }
    }
}