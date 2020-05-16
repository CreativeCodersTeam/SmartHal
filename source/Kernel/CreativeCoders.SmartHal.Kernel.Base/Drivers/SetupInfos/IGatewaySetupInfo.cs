using System.Collections.Generic;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Drivers.SetupInfos
{
    [PublicAPI]
    public interface IGatewaySetupInfo
    {
        GatewayId Id { get; }
        
        string GatewayType { get; }

        string Address { get; }
        
        T ReadSetting<T>(string name);

        T ReadSetting<T>(string name, T defaultValue);
        
        IDictionary<string, object> Settings { get; }
    }
}