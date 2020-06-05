using System.Threading.Tasks;
using CreativeCoders.Config.Base;
using CreativeCoders.SmartHal.Config.Base.Kernel;
using CreativeCoders.SmartHal.Kernel.Base.InitSystem;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Assemblies
{
    [UsedImplicitly]
    [InitSystemStep(typeof(IAssemblySubSystem))]
    public class AssemblyBootStep : IBootStep
    {
        private readonly IAssemblySubSystem _assemblySubSystem;
        
        private readonly IKernelConfiguration _kernelConfiguration;

        public AssemblyBootStep(ISetting<IKernelConfiguration> kernelConfiguration, IAssemblySubSystem assemblySubSystem)
        {
            _kernelConfiguration = kernelConfiguration.Value;
            _assemblySubSystem = assemblySubSystem;
        }
        
        public Task ExecuteAsync()
        {
            return _assemblySubSystem.LoadAssembliesAsync(_kernelConfiguration.GetAssemblyReferences());
        }
    }
}