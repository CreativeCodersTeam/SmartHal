using System.Collections.Generic;

namespace CreativeCoders.SmartHal.Config.Base.Kernel
{
    public interface IKernelConfiguration
    {
        IEnumerable<IKernelAssemblyReference> GetAssemblyReferences();
        
        string AssembliesBasePath { get; }
    }
}