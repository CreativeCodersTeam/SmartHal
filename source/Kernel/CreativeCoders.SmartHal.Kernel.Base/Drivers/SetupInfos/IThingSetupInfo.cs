using System.Collections.Generic;
using CreativeCoders.SmartHal.Kernel.Base.Things;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Drivers.SetupInfos
{
    [PublicAPI]
    public interface IThingSetupInfo
    {
        ThingId Id { get; }
        
        string Address { get; }
        
        IThingTemplate Template { get; }
        
        T ReadSetting<T>(string name);

        T ReadSetting<T>(string name, T defaultValue);
        
        IDictionary<string, object> Settings { get; }
    }
}