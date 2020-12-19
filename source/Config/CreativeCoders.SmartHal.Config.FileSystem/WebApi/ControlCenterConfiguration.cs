using System;
using System.Collections.Generic;
using CreativeCoders.SmartHal.Config.Base.WebApi;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.FileSystem.WebApi
{
    [UsedImplicitly]
    public class ControlCenterConfiguration : IControlCenterConfiguration
    {
        public IEnumerable<string> Urls { get; set; } = Array.Empty<string>();

        public int DefaultPort { get; set; } = 13579;

        public bool ListenOnLocalhost { get; set; } = true;

        public bool ListenOnHostName { get; set; } = true;
    }
}