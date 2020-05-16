using System;
using CreativeCoders.Config;
using CreativeCoders.Config.Base;
using CreativeCoders.Config.Base.Exceptions;
using CreativeCoders.Core;
using CreativeCoders.Core.Logging;

namespace CreativeCoders.SmartHal.Config.FileSystem.Building
{
    public class FileConfigurationBuilder
    {
        private static readonly ILogger Log = LogManager.GetLogger<FileConfigurationBuilder>();
        
        private readonly string _basePath;
        
        private readonly bool _haltOnConfigError;
        
        public FileConfigurationBuilder(string basePath, bool haltOnConfigError)
        {
            Ensure.DirectoryExists(basePath);
            
            _basePath = basePath;
            _haltOnConfigError = haltOnConfigError;
        }
        
        public IConfiguration Build()
        {
            Log.Info($"Initialize configuration system from base path '{_basePath}'");
            
            var config = new Configuration();

            config.OnSourceException(HandleConfigSourceError);
            
            config.InitializeFromAssembly(typeof(FileConfigurationBuilder).Assembly, _basePath);
            
            return config;
        }
        
        private void HandleConfigSourceError(IConfigurationSource source, Exception exception,
            SourceExceptionHandleResult handleResult)
        {
            handleResult.IsHandled = true;

            if (exception is ConfigurationFileSourceException fileException)
            {
                Log.Error($"Configuration file '{fileException.FileName}' error", exception);
            }
            else
            {
                Log.Error($"Configuration source '{source.GetType().Name}' error", exception);
            }

            if (_haltOnConfigError)
            {
                Environment.Exit(1);
            }
        }
    }
}