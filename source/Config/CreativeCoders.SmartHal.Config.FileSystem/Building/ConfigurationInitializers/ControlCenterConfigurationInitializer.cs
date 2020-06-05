using CreativeCoders.Config.Base;
using CreativeCoders.Config.Sources.Json;
using CreativeCoders.Core.IO;
using CreativeCoders.SmartHal.Config.FileSystem.WebApi;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.FileSystem.Building.ConfigurationInitializers
{
    [UsedImplicitly]
    public class ControlCenterConfigurationInitializer : IConfigurationInitializer
    {
        private const string ControlCenterConfigFileName = "controlcenter.config";

        private readonly string _basePath;

        public ControlCenterConfigurationInitializer(string basePath)
        {
            _basePath = basePath;
        }

        public void Configure(IConfiguration configuration)
        {
            configuration.AddSource(
                new JsonConfigurationSource<ControlCenterConfiguration>(FileSys.Path.Combine(_basePath,
                    ControlCenterConfigFileName)));
        }
    }
}