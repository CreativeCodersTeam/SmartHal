using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CreativeCoders.Core;
using CreativeCoders.Core.Logging;
using CreativeCoders.SmartHal.Config.Base.Kernel;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Assemblies
{
    [UsedImplicitly]
    public class AssemblyLoader : IAssemblyLoader
    {
        private static readonly ILogger Log = LogManager.GetLogger<AssemblyLoader>();
        
        private readonly IEnumerable<IAssemblyReferenceLoader> _referenceLoaders;

        public AssemblyLoader(IEnumerable<IAssemblyReferenceLoader> referenceLoaders)
        {
            _referenceLoaders = referenceLoaders;
        }
        
        public void Load(IKernelAssemblyReference assemblyReference, Action<Assembly> addAssembly)
        {
            var loader = _referenceLoaders.FirstOrDefault(x => x.AssemblyKind == assemblyReference.Kind);

            if (loader != null)
            {
                loader.LoadAssembly(assemblyReference.Reference, addAssembly);

                return;
            }
            
            Log.Warn($"No loader for assembly reference kind '{assemblyReference.Kind}' found");
        }

        public void Load(IEnumerable<IKernelAssemblyReference> assemblyReferences, Action<Assembly> addAssembly)
        {
            var assembliesCount = 0;
            
            assemblyReferences
                .ForEach(assemblyReference =>
                    Load(assemblyReference, assembly =>
                    {
                        addAssembly(assembly);
                        assembliesCount++;
                    }));
            
            Log.Info($"{assembliesCount} {GetAssemblyText(assembliesCount)} loaded");
        }
        
        private static string GetAssemblyText(int assemblyCount)
        {
            return assemblyCount == 1
                ? "assembly"
                : "assemblies";
        }
    }
}