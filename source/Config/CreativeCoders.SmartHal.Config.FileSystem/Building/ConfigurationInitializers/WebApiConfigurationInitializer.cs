using CreativeCoders.Config.Base;
using CreativeCoders.Config.Sources.Json;
using CreativeCoders.Core.IO;
using CreativeCoders.SmartHal.Config.FileSystem.WebApi;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.FileSystem.Building.ConfigurationInitializers
{
    [UsedImplicitly]
    public class WebApiConfigurationInitializer : IConfigurationInitializer
    {
        private const string WebApiConfigFileName = "webapi.config";

        private readonly string _basePath;

        public WebApiConfigurationInitializer(string basePath)
        {
            _basePath = basePath;
        }

        public void Configure(IConfiguration configuration)
        {
            configuration.AddSource(
                new JsonConfigurationSource<WebApiConfiguration>(FileSys.Path.Combine(_basePath,
                    WebApiConfigFileName)));
        }
    }
}