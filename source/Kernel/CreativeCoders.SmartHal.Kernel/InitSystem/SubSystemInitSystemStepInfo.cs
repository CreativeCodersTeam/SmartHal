using System;
using System.Reflection;
using CreativeCoders.SmartHal.Kernel.Base.InitSystem;

namespace CreativeCoders.SmartHal.Kernel.InitSystem
{
    public class SubSystemInitSystemStepInfo<T>
    {
        public SubSystemInitSystemStepInfo(T step)
        {
            Step = step;

            var bootStepAttribute = step.GetType().GetCustomAttribute<InitSystemStepAttribute>();

            if (bootStepAttribute == null)
            {
                throw new InvalidOperationException("Boot step is not assigned to a sub system");
            }

            SubSystemType = bootStepAttribute.SubSystemType;
        }

        internal SubSystemInitSystemStepInfo<T> SetName(string name)
        {
            Name = name;

            return this;
        }

        public T Step { get; }

        public Type SubSystemType { get; }

        public string Name { get; private set; }
    }
}