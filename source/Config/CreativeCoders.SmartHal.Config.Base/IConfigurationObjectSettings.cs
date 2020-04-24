using System.Collections.Generic;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.Base
{
    [PublicAPI]
    public interface IConfigurationObjectSettings
    {
        T ReadSetting<T>(string name);

        T ReadSetting<T>(string name, T defaultValue);
        
        IDictionary<string, object> Settings { get; }
    }
}