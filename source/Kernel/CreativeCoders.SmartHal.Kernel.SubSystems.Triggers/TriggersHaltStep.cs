using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.Halting;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Triggers
{
    [UsedImplicitly]
    public class TriggersHaltStep : ITriggersHaltStep
    {
        private readonly ITriggerRepository _triggerRepository;

        public TriggersHaltStep(ITriggerRepository triggerRepository)
        {
            _triggerRepository = triggerRepository;
        }
        
        public Task HaltAsync()
        {
            return _triggerRepository.ClearAsync();
        }
    }
}