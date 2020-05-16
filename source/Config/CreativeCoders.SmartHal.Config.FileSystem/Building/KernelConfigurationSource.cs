using CreativeCoders.Config.Sources.Json;
using CreativeCoders.Core.IO;
using CreativeCoders.SmartHal.Config.FileSystem.Kernel;

namespace CreativeCoders.SmartHal.Config.FileSystem.Building
{
    public class KernelConfigurationSource : JsonConfigurationSource<KernelConfiguration>
    {
        private readonly string _basePath;

        public KernelConfigurationSource(string basePath, string configFileName) : base(FileSys.Path.Combine(basePath, configFileName))
        {
            _basePath = basePath;
        }

        public override object GetSettingObject()
        {
            var settingObject = (KernelConfiguration) base.GetSettingObject();

            if (string.IsNullOrEmpty(settingObject.AssembliesBasePath))
            {
                settingObject.AssembliesBasePath = FileSys.Path.Combine(_basePath, "assemblies");
            }

            return settingObject;
        }
    }
}