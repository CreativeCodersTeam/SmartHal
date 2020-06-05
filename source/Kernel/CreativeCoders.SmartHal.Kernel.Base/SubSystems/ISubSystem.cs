using System;
using System.Collections.Generic;

namespace CreativeCoders.SmartHal.Kernel.Base.SubSystems
{
    public interface ISubSystem
    {
        string Name { get; }

        IReadOnlyCollection<Type> DependsOnSubSystems { get; }
    }
}