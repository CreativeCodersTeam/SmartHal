using System;

namespace CreativeCoders.SmartHal.Kernel.Base.SubSystems
{
    public class DependsOnAttribute : Attribute
    {
        public DependsOnAttribute(params Type[] subSystemTypes)
        {
            SubSystemTypes = subSystemTypes;
        }

        public Type[] SubSystemTypes { get; }
    }
}