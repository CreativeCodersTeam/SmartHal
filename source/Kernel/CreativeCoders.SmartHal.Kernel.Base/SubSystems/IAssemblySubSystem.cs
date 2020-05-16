using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.SmartHal.Config.Base.Kernel;

namespace CreativeCoders.SmartHal.Kernel.Base.SubSystems
{
    public interface IAssemblySubSystem
    {
        Task LoadAssembliesAsync(IEnumerable<IKernelAssemblyReference> assemblyReferences);
    }
}