using System;
using System.Collections.Generic;
using System.Reflection;

namespace CreativeCoders.SmartHal.Kernel.Base.SubSystems
{
    public abstract class SubSystemBase : ISubSystem
    {
        protected SubSystemBase()
        {
            ReadInfosFromAttributes();
        }

        private void ReadInfosFromAttributes()
        {
            var subSystemAttribute = GetType().GetCustomAttribute<SubSystemAttribute>();

            if (subSystemAttribute == null)
            {
                throw new InvalidOperationException("SubSystem must have the SubSystemAttribute");
            }

            Name = subSystemAttribute.Name;

            var dependsOnAttribute = GetType().GetCustomAttribute<DependsOnAttribute>();

            DependsOnSubSystems = dependsOnAttribute?.SubSystemTypes ?? Array.Empty<Type>();
        }

        public string Name { get; private set; }

        public IReadOnlyCollection<Type> DependsOnSubSystems { get; private set; }
    }
}