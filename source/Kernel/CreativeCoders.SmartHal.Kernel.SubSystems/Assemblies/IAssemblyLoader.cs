using System;
using System.Collections.Generic;
using System.Reflection;
using CreativeCoders.SmartHal.Config.Base.Kernel;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Assemblies
{
    [PublicAPI]
    public interface IAssemblyLoader
    {
        void Load(IKernelAssemblyReference assemblyReference, Action<Assembly> addAssembly);
        
        void Load(IEnumerable<IKernelAssemblyReference> assemblyReferences, Action<Assembly> addAssembly);
    }
}