using System.Collections.Generic;
using System.Linq;
using CreativeCoders.Config.Base;
using CreativeCoders.Core.IO;
using CreativeCoders.SmartHal.Config.FileSystem.Scripts;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.FileSystem.Building.ConfigurationInitializers
{
    [UsedImplicitly]
    public class ActionScriptDataInitializer : ConfigurationInitializerBase<ActionScriptData>
    {
        public ActionScriptDataInitializer(string basePath) : base(basePath, "scripts", "*.action")
        {
        }

        protected override IEnumerable<IConfigurationSource<ActionScriptData>> GetSources(string path, string fileSearchPattern)
        {
            return FileSys.Directory.EnumerateFiles(path, fileSearchPattern)
                .Select(CreateActionScriptData);
        }

        private IConfigurationSource<ActionScriptData> CreateActionScriptData(string fileName)
        {
            return new ScriptDataConfigurationSource<ActionScriptData>(fileName);
        }
    }
}