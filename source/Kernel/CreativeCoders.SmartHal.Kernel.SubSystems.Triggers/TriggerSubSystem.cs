using System;
using System.Threading.Tasks;
using CreativeCoders.Core.Threading;
using CreativeCoders.SmartHal.Kernel.Base.Messaging;
using CreativeCoders.SmartHal.Kernel.Base.Repositories;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using CreativeCoders.SmartHal.Kernel.Base.Triggers;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Triggers
{
    [UsedImplicitly]
    [SubSystem("Triggers")]
    [DependsOn(typeof(IItemSubSystem))]
    public class TriggerSubSystem : SubSystemBase, ITriggerSubSystem
    {
        private readonly IMessageHub _messageHub;
        
        private readonly IItemRepository _itemRepository;
        
        private readonly ITriggerRepository _triggerRepository;
        
        public TriggerSubSystem(IMessageHub messageHub, IItemRepository itemRepository, ITriggerRepository triggerRepository)
        {
            _messageHub = messageHub;
            _itemRepository = itemRepository;
            _triggerRepository = triggerRepository;
        }
        
        public IItemChangedTrigger CreateItemChangedTrigger(string itemName, object oldValue, object newValue, Func<Task> executeAsync)
        {
            var trigger = new ItemChangedTrigger(_itemRepository, _messageHub, itemName, oldValue, newValue, executeAsync);

            _triggerRepository.AddAsync(trigger).FireAndForgetAsync(_ => { });

            return trigger;
        }
    }
}