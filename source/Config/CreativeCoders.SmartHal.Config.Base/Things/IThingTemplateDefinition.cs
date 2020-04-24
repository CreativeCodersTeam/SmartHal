using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.Base.Things
{
    [PublicAPI]
    public interface IThingTemplateDefinition : IConfigurationObject, IConfigurationObjectSettings
    {
        string[] Channels { get; }
    }
}