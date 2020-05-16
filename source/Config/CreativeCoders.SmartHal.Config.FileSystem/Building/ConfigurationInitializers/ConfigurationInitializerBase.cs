using System.Collections.Generic;
using CreativeCoders.Config.Base;
using CreativeCoders.Config.Sources.Json;
using CreativeCoders.Core.IO;

namespace CreativeCoders.SmartHal.Config.FileSystem.Building.ConfigurationInitializers
{
    public abstract class ConfigurationInitializerBase<T> : IConfigurationInitializer
        where T : class, new()
    {
        private readonly string _path;
        
        private readonly string _fileSearchPattern;

        protected ConfigurationInitializerBase(string basePath, string configFolderName, string fileSearchPattern)
        {
            _path = FileSys.Path.Combine(basePath, configFolderName);
            _fileSearchPattern = fileSearchPattern;
        }

        protected virtual IEnumerable<IConfigurationSource<T>> GetSources(string path, string fileSearchPattern)
        {
            return JsonConfigurationSource<T>.FromFiles(
                FileSys.Directory.EnumerateFiles(path, fileSearchPattern));
        }
        
        public void Configure(IConfiguration configuration)
        {
            configuration.AddSources(GetSources(_path, _fileSearchPattern));
        }
    }
}