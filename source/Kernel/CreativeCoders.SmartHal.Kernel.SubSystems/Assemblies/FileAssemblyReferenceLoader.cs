using System;
using System.Reflection;
using CreativeCoders.Config.Base;
using CreativeCoders.Core.IO;
using CreativeCoders.Core.Logging;
using CreativeCoders.Core.SysEnvironment;
using CreativeCoders.SmartHal.Config.Base.Kernel;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Assemblies
{
    public class FileAssemblyReferenceLoader : IAssemblyReferenceLoader
    {
        private static readonly ILogger Log = LogManager.GetLogger<FileAssemblyReferenceLoader>();
        
        private readonly string _assembliesBasePath;

        public FileAssemblyReferenceLoader(ISetting<IKernelConfiguration> kernelConfiguration)
        {
            _assembliesBasePath = kernelConfiguration.Value.AssembliesBasePath;
        }
        
        public void LoadAssembly(string assemblyReference, Action<Assembly> addAssembly)
        {
            var fileName = FileSys.Path.Combine(Env.GetAppDirectory(), assemblyReference + ".dll");

            if (!FileSys.File.Exists(fileName))
            {
                Log.Info($"Assembly '{assemblyReference}' not found in application directory");

                fileName = FileSys.Path.Combine(_assembliesBasePath, assemblyReference + ".dll");

                if (!FileSys.File.Exists(fileName))
                {
                    Log.Info($"Assembly '{assemblyReference}' not found in assemblies directory '{_assembliesBasePath}'");
                    
                    return;
                }
            }
            
            Log.Info($"Loading kernel assembly '{fileName}'");
            
            var assembly = Assembly.LoadFile(fileName);

            addAssembly(assembly);
        }

        public string AssemblyKind => "File";
    }
}