using System;
using System.Diagnostics.CodeAnalysis;

namespace CreativeCoders.SmartHal.Kernel.Base.Drivers
{
    [ExcludeFromCodeCoverage]
    public class DriverInfo
    {
        public DriverInfo(Type driverType)
        {
            DriverType = driverType;
        }
        
        public string Name { get; set; }

        public string Version { get; set; }

        public Type DriverType { get; }
    }
}