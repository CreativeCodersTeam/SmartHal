using System.Threading.Tasks;
using CreativeCoders.Config.Base;
using CreativeCoders.SmartHal.Config.Base.Kernel;
using CreativeCoders.SmartHal.Kernel.Base.Booting;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Assemblies
{
    [UsedImplicitly]
    public class AssemblyBootStep : IAssemblyBootStep
    {
        private readonly IAssemblySubSystem _assemblySubSystem;
        
        private readonly IKernelConfiguration _kernelConfiguration;

        public AssemblyBootStep(ISetting<IKernelConfiguration> kernelConfiguration, IAssemblySubSystem assemblySubSystem)
        {
            _kernelConfiguration = kernelConfiguration.Value;
            _assemblySubSystem = assemblySubSystem;
        }
        
        public Task LoadAssembliesAsync()
        {
            return _assemblySubSystem.LoadAssembliesAsync(_kernelConfiguration.GetAssemblyReferences());
        }
    }
}