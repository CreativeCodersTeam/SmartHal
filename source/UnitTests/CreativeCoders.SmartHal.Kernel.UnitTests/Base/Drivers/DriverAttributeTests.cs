using CreativeCoders.SmartHal.Kernel.Base.Drivers;
using Xunit;

namespace CreativeCoders.SmartHal.Kernel.UnitTests.Base.Drivers
{
    public class DriverAttributeTests
    {
        [Fact]
        public void GetName_NameViaCtor_ReturnsName()
        {
            var attribute = new DriverAttribute("TestDriver");

            var driverInfo = attribute.CreateDriverInfo(typeof(DriverAttributeTests));
            
            Assert.Equal("TestDriver", driverInfo.Name);
        }
        
        [Fact]
        public void GetName_NameNull_ReturnsTypeFullName()
        {
            var attribute = new DriverAttribute();
            
            var driverInfo = attribute.CreateDriverInfo(typeof(DriverAttributeTests));
            
            Assert.Equal(typeof(DriverAttributeTests).FullName, driverInfo.Name);
        }
        
        [Fact]
        public void GetVersion_VersionViaCtor_ReturnsVersion()
        {
            var attribute = new DriverAttribute("TestDriver") {Version = "1.0"};
            
            var driverInfo = attribute.CreateDriverInfo(typeof(DriverAttributeTests));

            Assert.Equal("1.0", driverInfo.Version);
        }
        
        [Fact]
        public void GetVersion_VersionNull_ReturnsAssemblyVersion()
        {
            var attribute = new DriverAttribute("TestDriver");
            
            var driverInfo = attribute.CreateDriverInfo(typeof(DriverAttributeTests));

            Assert.Equal(typeof(DriverAttributeTests).Assembly.GetName().Version?.ToString(), driverInfo.Version);
        }
    }
}