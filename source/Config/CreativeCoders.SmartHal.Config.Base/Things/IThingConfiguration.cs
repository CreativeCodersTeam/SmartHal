using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.Base.Things
{
    [PublicAPI]
    public interface IThingConfiguration : IConfigurationObject, IConfigurationObjectSettings
    {
        string GatewayName { get; }

        string Address { get; }

        string Template { get; }
    }
}