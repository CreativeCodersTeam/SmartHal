using System.Collections.Generic;
using System.Linq;
using CreativeCoders.Config.Base;
using CreativeCoders.Core.IO;
using CreativeCoders.SmartHal.Config.FileSystem.Drivers;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.FileSystem.Building.ConfigurationInitializers
{
    [UsedImplicitly]
    public class DriverConfigurationInitializer : ConfigurationInitializerBase<DriverConfiguration>
    {
        public DriverConfigurationInitializer(string basePath) : base(basePath, "drivers", "*.driver")
        {
        }

        protected override IEnumerable<IConfigurationSource<DriverConfiguration>> GetSources(string path, string fileSearchPattern)
        {
            return FileSys.Directory.EnumerateFiles(path, fileSearchPattern)
                .Select(CreateDriverSource);
        }

        private static IConfigurationSource<DriverConfiguration> CreateDriverSource(string file)
        {
            return new DriverConfigurationSource(file);
        }
    }
}