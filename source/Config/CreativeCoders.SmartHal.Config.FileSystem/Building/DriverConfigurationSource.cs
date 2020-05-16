using CreativeCoders.Config.Sources.Json;
using CreativeCoders.Core.IO;
using CreativeCoders.SmartHal.Config.FileSystem.Drivers;

namespace CreativeCoders.SmartHal.Config.FileSystem.Building
{
    public class DriverConfigurationSource : JsonConfigurationSource<DriverConfiguration>
    {
        private readonly string _jsonFileName;

        public DriverConfigurationSource(string jsonFileName) : base(jsonFileName)
        {
            _jsonFileName = jsonFileName;
        }

        public override object GetSettingObject()
        {
            var settingObject = (DriverConfiguration) base.GetSettingObject();

            if (string.IsNullOrWhiteSpace(settingObject.Name))
            {
                settingObject.Name = FileSys.Path.GetFileNameWithoutExtension(_jsonFileName);
            }

            return settingObject;
        }
    }
}