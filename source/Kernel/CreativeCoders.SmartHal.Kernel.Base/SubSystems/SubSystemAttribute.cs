using System;

namespace CreativeCoders.SmartHal.Kernel.Base.SubSystems
{
    public class SubSystemAttribute : Attribute
    {
        public SubSystemAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}