using System;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Drivers
{
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DriverAttribute : Attribute
    {
        public DriverAttribute()
        {
        }

        public DriverAttribute(string name)
        {
            Name = name;
        }

        public DriverInfo CreateDriverInfo(Type driverType)
        {
            return new DriverInfo(driverType)
            {
                Name = GetName(driverType),
                Version = GetVersion(driverType)
            };
        }

        private string GetName(Type driverType)
        {
            return string.IsNullOrWhiteSpace(Name)
                ? driverType.FullName
                : Name;
        }

        private string GetVersion(Type driverType)
        {
            return string.IsNullOrWhiteSpace(Version)
                ? driverType.Assembly.GetName().Version.ToString()
                : Version;
        }
        
        public string Name { get; }
        
        public string Version { get; set; }
    }
}