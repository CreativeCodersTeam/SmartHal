using System;

namespace CreativeCoders.SmartHal.Kernel.Base.InitSystem
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InitSystemStepAttribute : Attribute
    {
        public InitSystemStepAttribute(Type subSystemType)
        {
            SubSystemType = subSystemType;
        }

        public Type SubSystemType { get; }
    }
}