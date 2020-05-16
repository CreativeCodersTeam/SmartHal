using CreativeCoders.Config.Base;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.FileSystem.Building.ConfigurationInitializers
{
    [UsedImplicitly]
    public class KernelConfigurationInitializer : IConfigurationInitializer
    {
        private readonly string _basePath;
        
        public KernelConfigurationInitializer(string basePath)
        {
            _basePath = basePath;
            
        }
        
        public void Configure(IConfiguration configuration)
        {
            configuration.AddSource(new KernelConfigurationSource(_basePath, "kernel.config"));
        }
    }
}