using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.Base.Scripts
{
    [PublicAPI]
    public interface IScriptData : IConfigurationObject
    {
        string SourceCode { get; }
    }
}