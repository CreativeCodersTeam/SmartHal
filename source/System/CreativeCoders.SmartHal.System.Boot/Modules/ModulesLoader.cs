using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.Core.IO;
using CreativeCoders.Core.Logging;
using CreativeCoders.Core.SysEnvironment;

namespace CreativeCoders.SmartHal.System.Boot.Modules
{
    public class ModulesLoader
    {
        private static readonly ILogger Log = LogManager.GetLogger<ModulesLoader>();

        private readonly string _instancePath;

        public ModulesLoader(string instancePath)
        {
            Ensure.IsNotNullOrWhitespace(instancePath, nameof(instancePath));
            
            _instancePath = instancePath;
        }
        
        public async Task LoadAllModulesAsync()
        {
            var config = await LoadConfigurationAsync();
            
            config.Modules
                .ForEach(x => LoadAssembly(x.Reference, config.ModulesBasePath));
        }
        
        private async Task<ModulesConfiguration> LoadConfigurationAsync()
        {
            await using var configFileStream =
                FileSys.File.OpenRead(FileSys.Path.Combine(_instancePath, "modules.config"));

            return await JsonSerializer.DeserializeAsync<ModulesConfiguration>(configFileStream);
        }

        public void LoadAssembly(string assemblyReference, string assembliesBasePath)
        {
            var fileName = FileSys.Path.Combine(Env.GetAppDirectory(), assemblyReference + ".dll");

            if (!FileSys.File.Exists(fileName))
            {
                Log.Info($"Assembly '{assemblyReference}' not found in application directory");

                fileName = FileSys.Path.Combine(assembliesBasePath, assemblyReference + ".dll");

                if (!FileSys.File.Exists(fileName))
                {
                    Log.Info($"Assembly '{assemblyReference}' not found in assemblies directory '{assembliesBasePath}'");

                    return;
                }
            }

            Log.Info($"Loading kernel assembly '{fileName}'");

            var assembly = Assembly.LoadFile(fileName);
            
            Log.Info($"Assembly loaded '{assembly.FullName}'");
        }
    }
}