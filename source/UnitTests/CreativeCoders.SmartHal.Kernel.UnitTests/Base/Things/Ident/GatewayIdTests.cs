using System;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;
using Xunit;

namespace CreativeCoders.SmartHal.Kernel.UnitTests.Base.Things.Ident
{
    public class GatewayIdTests
    {
        [Fact]
        public void ToString_CreateId_ResultOk()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            
            var gatewayId = new GatewayId(driver, gateway);
            
            Assert.Equal($"{driver}:{gateway}", gatewayId.ToString());
        }
        
        [Fact]
        public void Gateway_CreateId_GetValueOk()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            
            var gatewayId = new GatewayId(driver, gateway);
            
            Assert.Equal(gateway, gatewayId.Gateway);
        }
        
        [Fact]
        public void Driver_CreateId_GetValueOk()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            
            var gatewayId = new GatewayId(driver, gateway);
            
            Assert.Equal(driver, gatewayId.Driver);
        }

        [Fact]
        public void Equals_SameIdString_ReturnsTrue()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            
            var gatewayId = new GatewayId(driver, gateway);
            
            Assert.True(gatewayId.Equals($"{driver}:{gateway}"));
        }
        
        [Fact]
        public void Equals_NotSameIdString_ReturnsFalse()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string driver2 = "Driver2";
            
            var gatewayId = new GatewayId(driver, gateway);
            
            Assert.False(gatewayId.Equals($"{driver2}:{gateway}"));
        }
        
        [Fact]
        public void Equals_SameId_ReturnsTrue()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            
            var gatewayId = new GatewayId(driver, gateway);
            
            Assert.True(gatewayId.Equals(gatewayId));
        }
        
        [Fact]
        public void Equals_OtherIdWithSameValue_ReturnsTrue()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            
            var gatewayId = new GatewayId(driver, gateway);
            var gatewayId2 = new GatewayId(driver, gateway);
            
            Assert.True(gatewayId.Equals(gatewayId2));
        }
        
        [Fact]
        public void Equals_NullId_ReturnsFalse()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            
            var gatewayId = new GatewayId(driver, gateway);

            Assert.False(gatewayId.Equals((IdBase) null));
        }
        
        [Fact]
        public void Equals_SameIdStringObject_ReturnsTrue()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            
            var gatewayId = new GatewayId(driver, gateway);

            object id = $"{driver}:{gateway}";

            Assert.True(gatewayId.Equals(id));
        }
        
        [Fact]
        public void Equals_NotSameIdStringObject_ReturnsFalse()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string driver2 = "Driver2";
            
            var gatewayId = new GatewayId(driver, gateway);
            
            object id = $"{driver2}:{gateway}";
            
            Assert.False(gatewayId.Equals(id));
        }
        
        [Fact]
        public void Equals_SameIdObject_ReturnsTrue()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            
            var gatewayId = new GatewayId(driver, gateway);

            object id = gatewayId; 
            
            Assert.True(gatewayId.Equals(id));
        }
        
        [Fact]
        public void Equals_OtherIdWithSameValueObject_ReturnsTrue()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            
            var gatewayId = new GatewayId(driver, gateway);
            var gatewayId2 = new GatewayId(driver, gateway);

            object id = gatewayId2; 
            
            Assert.True(gatewayId.Equals(id));
        }

        [Fact]
        public void GetHashCode_GatewayId_EqualsIdStringHashCode()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            
            var gatewayId = new GatewayId(driver, gateway);
            
            Assert.Equal(gatewayId.ToString().GetHashCode(), gatewayId.GetHashCode());
        }

        [Fact]
        public void TryParse_ValidIdString_ReturnsTrueAndOutGatewayId()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";

            var parseOk = GatewayId.TryParse($"{driver}:{gateway}", out var gatewayId);
            
            Assert.True(parseOk);
            Assert.Equal(driver, gatewayId.Driver);
            Assert.Equal(gateway, gatewayId.Gateway);
        }
        
        [Fact]
        public void TryParse_InvalidIdString_ReturnsFalseAndGatewayIdNull()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";

            var parseOk = GatewayId.TryParse($"{driver}:{gateway}:Thing", out var gatewayId);
            
            Assert.False(parseOk);
            Assert.Null(gatewayId);
        }
        
        [Fact]
        public void Parse_ValidIdString_ReturnsGatewayId()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";

            var gatewayId = GatewayId.Parse($"{driver}:{gateway}");
            
            Assert.Equal(driver, gatewayId.Driver);
            Assert.Equal(gateway, gatewayId.Gateway);
        }
        
        [Fact]
        public void Parse_InvalidIdString_ThrowsException()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";

            Assert.Throws<FormatException>(() => GatewayId.Parse($"{driver}:{gateway}:Thing"));
        }

        [Fact]
        public void ImplicitEquals_SameId_ReturnsTrue()
        {
            const string driver = "TestDriver";
            const string gateway = "TestGateway";

            var gatewayId1 = new GatewayId(driver, gateway);

            var gatewayId2 = new GatewayId(driver, gateway);

            Assert.Equal(gatewayId1, gatewayId2);

            Assert.True(gatewayId1 == gatewayId2);
        }

        [Fact]
        public void ImplicitNotEquals_SameId_ReturnsFalse()
        {
            const string driver = "TestDriver";
            const string gateway = "TestGateway";

            var gatewayId1 = new GatewayId(driver, gateway);

            var gatewayId2 = new GatewayId(driver, gateway);

            Assert.False(gatewayId1 != gatewayId2);
        }
    }
}