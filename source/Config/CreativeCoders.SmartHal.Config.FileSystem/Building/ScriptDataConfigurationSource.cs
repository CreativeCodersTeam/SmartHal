using CreativeCoders.Config.Sources;
using CreativeCoders.Core.IO;
using CreativeCoders.SmartHal.Config.FileSystem.Scripts;

namespace CreativeCoders.SmartHal.Config.FileSystem.Building
{
    public class ScriptDataConfigurationSource<T> : ConfigurationSource<T>
        where T : ScriptData, new()
    {
        public ScriptDataConfigurationSource(string scriptFileName) : base(() => LoadFromScriptFile(scriptFileName))
        {
        }
        
        private static T LoadFromScriptFile(string fileName)
        {
            var scriptData = new T
            {
                Name = FileSys.Path.GetFileNameWithoutExtension(fileName),
                SourceCode = FileSys.File.ReadAllText(fileName)
            };
            return scriptData;
        }
    }
}