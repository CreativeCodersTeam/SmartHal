using System;
using System.Reflection;
using CreativeCoders.Core.IO;
using CreativeCoders.Core.SysEnvironment;
using CreativeCoders.Logging.Nlog;

namespace CreativeCoders.SmartHal.Daemon.Base
{
    public static class Logging
    {
        public static void InitNlog(string logPath)
        {
            var configPath = FileSys.Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? Env.CurrentDirectory;
            if (string.IsNullOrWhiteSpace(configPath))
            {
                configPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            }
            var nlogConfigFile = FileSys.Path.Combine(configPath, "NLog.config");
            if (!FileSys.File.Exists(nlogConfigFile))
            {
                throw new ApplicationException("NLog.config not found");
            }
            Nlog.Init(nlogConfigFile);
            NLog.LogManager.Configuration.Variables["LogPath"] = logPath;
        }
    }
}