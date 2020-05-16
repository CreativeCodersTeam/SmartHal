using System.Collections.Generic;
using CreativeCoders.SmartHal.Config.Base.Kernel;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.FileSystem.Kernel
{
    [PublicAPI]
    public class KernelConfiguration : IKernelConfiguration
    {
        public IEnumerable<IKernelAssemblyReference> GetAssemblyReferences()
        {
            return AssemblyReferences;
        }

        public string AssembliesBasePath { get; set; } = string.Empty;

        public IEnumerable<KernelAssemblyReference> AssemblyReferences { get; set; } = new List<KernelAssemblyReference>();
    }
}