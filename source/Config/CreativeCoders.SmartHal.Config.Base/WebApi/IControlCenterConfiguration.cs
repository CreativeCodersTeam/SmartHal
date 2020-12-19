using System.Collections.Generic;

namespace CreativeCoders.SmartHal.Config.Base.WebApi
{
    public interface IControlCenterConfiguration
    {
        IEnumerable<string> Urls { get; }

        int DefaultPort { get; }

        bool ListenOnLocalhost { get; }

        bool ListenOnHostName { get; }
    }
}