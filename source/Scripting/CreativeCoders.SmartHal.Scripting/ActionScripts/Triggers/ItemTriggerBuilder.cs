using System;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using CreativeCoders.SmartHal.Scripting.Base.ActionScripts.Triggers;

namespace CreativeCoders.SmartHal.Scripting.ActionScripts.Triggers
{
    public class ItemTriggerBuilder : IItemTriggerBuilder
    {
        private readonly string _itemName;
        
        private readonly Action<ITriggerBuilder> _addBuilder;
        
        private readonly ITriggerSubSystem _triggerSubSystem;

        public ItemTriggerBuilder(string itemName, Action<ITriggerBuilder> addBuilder, ITriggerSubSystem triggerSubSystem)
        {
            _itemName = itemName;
            _addBuilder = addBuilder;
            _triggerSubSystem = triggerSubSystem;
        }

        public IItemChangedTriggerBuilder Changed()
        {
            var builder = new ItemChangedTriggerBuilder(_itemName, _triggerSubSystem);

            _addBuilder(builder);

            return builder;
        }
    }
}