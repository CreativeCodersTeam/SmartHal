using System;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using CreativeCoders.SmartHal.Scripting.Base.ActionScripts.Triggers;

namespace CreativeCoders.SmartHal.Scripting.ActionScripts.Triggers
{
    public class ItemChangedTriggerBuilder : IItemChangedTriggerBuilder, ITriggerBuilder
    {
        private readonly string _itemName;
        
        private readonly ITriggerSubSystem _triggerSubSystem;

        private object _fromValue;
        
        private object _toValue;

        public ItemChangedTriggerBuilder(string itemName, ITriggerSubSystem triggerSubSystem)
        {
            _itemName = itemName;
            _triggerSubSystem = triggerSubSystem;
        }

        public IItemChangedTriggerBuilder From(object value)
        {
            _fromValue = value;

            return this;
        }

        public IItemChangedTriggerBuilder To(object value)
        {
            _toValue = value;

            return this;
        }

        public void Build(Func<Task> executeAsync)
        {
            _triggerSubSystem.CreateItemChangedTrigger(_itemName, _fromValue, _toValue, executeAsync);
        }
    }
}