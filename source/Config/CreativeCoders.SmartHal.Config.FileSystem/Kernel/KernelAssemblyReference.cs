using CreativeCoders.SmartHal.Config.Base.Kernel;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.FileSystem.Kernel
{
    [UsedImplicitly]
    public class KernelAssemblyReference : IKernelAssemblyReference
    {
        public string Kind { get; [UsedImplicitly] set; }
        
        public string Reference { get; [UsedImplicitly] set; }
    }
}