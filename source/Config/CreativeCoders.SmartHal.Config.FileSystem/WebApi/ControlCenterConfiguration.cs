using System;
using CreativeCoders.SmartHal.Config.Base.WebApi;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Config.FileSystem.WebApi
{
    [UsedImplicitly]
    public class ControlCenterConfiguration : IControlCenterConfiguration
    {
        public string[] Urls { get; set; } = Array.Empty<string>();

        public int DefaultPort { get; set; } = 13579;

        public bool ListenOnLocalhost { get; set; } = true;

        public bool ListenOnHostName { get; set; } = true;
    }
}