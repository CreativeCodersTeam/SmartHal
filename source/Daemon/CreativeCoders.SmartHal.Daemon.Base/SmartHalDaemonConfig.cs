using CreativeCoders.Core.IO;

namespace CreativeCoders.SmartHal.Daemon.Base
{
    public class SmartHalDaemonConfig
    {
        private string _logPath;

        public string ConfigBasePath { get; set; }

        public string LogPath
        {
            get => string.IsNullOrWhiteSpace(_logPath) ? FileSys.Path.Combine(ConfigBasePath, "logs") : _logPath;
            set => _logPath = value;
        }
    }
}