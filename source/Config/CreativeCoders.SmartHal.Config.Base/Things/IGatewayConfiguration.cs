using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.Base.Things
{
    [PublicAPI]
    public interface IGatewayConfiguration : IConfigurationObject, IConfigurationObjectSettings
    {
        string GatewayType { get; }

        string DriverName { get; }

        string Address { get; }
    }
}