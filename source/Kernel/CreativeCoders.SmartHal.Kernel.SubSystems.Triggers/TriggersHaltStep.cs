using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.InitSystem;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Triggers
{
    [UsedImplicitly]
    [InitSystemStep(typeof(ITriggerSubSystem))]
    public class TriggersHaltStep : IHaltStep
    {
        private readonly ITriggerRepository _triggerRepository;

        public TriggersHaltStep(ITriggerRepository triggerRepository)
        {
            _triggerRepository = triggerRepository;
        }
        
        public async Task ExecuteAsync()
        {
            await _triggerRepository.ClearAsync().ConfigureAwait(false);
        }
    }
}