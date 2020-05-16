﻿using System;
 using System.Threading.Tasks;
 using CreativeCoders.SmartHal.Kernel.Base.Triggers;
 using JetBrains.Annotations;

 namespace CreativeCoders.SmartHal.Kernel.Base.SubSystems
{
    [PublicAPI]
    public interface ITriggerSubSystem
    {
        IItemChangedTrigger CreateItemChangedTrigger(string itemName, object oldValue, object newValue, Func<Task> executeAsync);
    }
}