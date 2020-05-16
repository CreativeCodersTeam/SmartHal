using System;
using System.Reflection;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Assemblies
{
    public interface IAssemblyReferenceLoader
    {
        void LoadAssembly(string assemblyReference, Action<Assembly> addAssembly);
        
        string AssemblyKind { get; }
    }
}