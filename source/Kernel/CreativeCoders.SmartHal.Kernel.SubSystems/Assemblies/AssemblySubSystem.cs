using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Config.Base.Kernel;
using CreativeCoders.SmartHal.Kernel.Base.SubSystems;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Assemblies
{
    [UsedImplicitly]
    [SubSystem("Assemblies")]
    public class AssemblySubSystem : SubSystemBase, IAssemblySubSystem
    {
        private readonly IAssemblyLoader _assemblyLoader;
        
        public AssemblySubSystem(IAssemblyLoader assemblyLoader)
        {
            _assemblyLoader = assemblyLoader;
        }

        public Task LoadAssembliesAsync(IEnumerable<IKernelAssemblyReference> assemblyReferences)
        {
            _assemblyLoader.Load(assemblyReferences, assembly => { });
            
            return Task.CompletedTask;
        }
    }
}