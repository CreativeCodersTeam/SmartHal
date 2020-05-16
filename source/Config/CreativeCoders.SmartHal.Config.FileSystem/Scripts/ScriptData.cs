using CreativeCoders.SmartHal.Config.Base.Scripts;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.FileSystem.Scripts
{
    [PublicAPI]
    public class ScriptData : IScriptData
    {
        public string Name { get; set; }
        
        public string SourceCode { get; set; }
    }
}