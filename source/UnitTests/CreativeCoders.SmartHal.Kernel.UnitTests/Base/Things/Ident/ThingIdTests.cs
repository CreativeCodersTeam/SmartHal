using System;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;
using Xunit;

namespace CreativeCoders.SmartHal.Kernel.UnitTests.Base.Things.Ident
{
    public class ThingIdTests
    {
        [Fact]
        public void ToString_CreateId_ResultOk()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string driver2 = "Driver2";
            const string gateway2 = "Gateway2";
            const string thing2 = "Thing2";

            var thingId = new ThingId(new GatewayId(driver, gateway), thing);
            
            Assert.Equal($"{driver}:{gateway}:{thing}", thingId.ToString());

            thingId.Driver = driver2;
            thingId.Gateway = gateway2;
            thingId.Thing = thing2;
            
            Assert.Equal($"{driver2}:{gateway2}:{thing2}", thingId.ToString());
        }
        
        [Fact]
        public void Thing_CreateId_GetValueOk()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string thing2 = "Thing2";
            
            var thingId = new ThingId(new GatewayId(driver, gateway), thing);
            
            Assert.Equal(thing, thingId.Thing);

            thingId.Thing = thing2;
            
            Assert.Equal(thing2, thingId.Thing);
        }
        
        [Fact]
        public void Gateway_CreateId_GetValueOk()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string gateway2 = "Gateway2";
            
            var thingId = new ThingId(new GatewayId(driver, gateway), thing);
            
            Assert.Equal(gateway, thingId.Gateway);

            thingId.Gateway = gateway2;
            
            Assert.Equal(gateway2, thingId.Gateway);
        }
        
        [Fact]
        public void Driver_CreateId_GetValueOk()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string driver2 = "Driver2";
            
            var thingId = new ThingId(new GatewayId(driver, gateway), thing);
            
            Assert.Equal(driver, thingId.Driver);

            thingId.Driver = driver2;
            
            Assert.Equal(driver2, thingId.Driver);
        }

        [Fact]
        public void Equals_SameIdString_ReturnsTrue()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            
            var thingId = new ThingId(new GatewayId(driver, gateway), thing);
            
            Assert.True(thingId.Equals($"{driver}:{gateway}:{thing}"));
        }
        
        [Fact]
        public void Equals_NotSameIdString_ReturnsFalse()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string driver2 = "Driver2";
            
            var thingId = new ThingId(new GatewayId(driver, gateway), thing);
            
            Assert.False(thingId.Equals($"{driver2}:{gateway}:{thing}"));
        }
        
        [Fact]
        public void Equals_SameId_ReturnsTrue()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            
            var thingId = new ThingId(new GatewayId(driver, gateway), thing);
            
            Assert.True(thingId.Equals(thingId));
        }
        
        [Fact]
        public void Equals_OtherIdWithSameValue_ReturnsTrue()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            
            var thingId = new ThingId(new GatewayId(driver, gateway), thing);
            var thingId2 = new ThingId(new GatewayId(driver, gateway), thing);
            
            Assert.True(thingId.Equals(thingId2));
        }
        
        [Fact]
        public void Equals_NullId_ReturnsFalse()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            
            var thingId = new ThingId(new GatewayId(driver, gateway), thing);

            Assert.False(thingId.Equals((IdBase) null));
        }
        
        [Fact]
        public void Equals_SameIdStringObject_ReturnsTrue()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            
            var thingId = new ThingId(new GatewayId(driver, gateway), thing);

            object id = $"{driver}:{gateway}:{thing}";

            Assert.True(thingId.Equals(id));
        }
        
        [Fact]
        public void Equals_NotSameIdStringObject_ReturnsFalse()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string driver2 = "Driver2";
            
            var thingId = new ThingId(new GatewayId(driver, gateway), thing);
            
            object id = $"{driver2}:{gateway}:{thing}";
            
            Assert.False(thingId.Equals(id));
        }
        
        [Fact]
        public void Equals_SameIdObject_ReturnsTrue()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            
            var thingId = new ThingId(new GatewayId(driver, gateway), thing);

            object id = thingId; 
            
            Assert.True(thingId.Equals(id));
        }
        
        [Fact]
        public void Equals_OtherIdWithSameValueObject_ReturnsTrue()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            
            var thingId = new ThingId(new GatewayId(driver, gateway), thing);
            var thingId2 = new ThingId(new GatewayId(driver, gateway), thing);

            object id = thingId2; 
            
            Assert.True(thingId.Equals(id));
        }

        [Fact]
        public void GetHashCode_ThingId_EqualsIdStringHashCode()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            
            var thingId = new ThingId(new GatewayId(driver, gateway), thing);
            
            Assert.Equal(thingId.ToString().GetHashCode(), thingId.GetHashCode());
        }

        [Fact]
        public void TryParse_ValidIdString_ReturnsTrueAndOutThingId()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";

            var parseOk = ThingId.TryParse($"{driver}:{gateway}:{thing}", out var thingId);
            
            Assert.True(parseOk);
            Assert.Equal(driver, thingId.Driver);
            Assert.Equal(gateway, thingId.Gateway);
            Assert.Equal(thing, thingId.Thing);
        }
        
        [Fact]
        public void TryParse_InvalidIdString_ReturnsFalseAndThingIdNull()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";

            var parseOk = ThingId.TryParse($"{driver}:{gateway}:{thing}:1", out var thingId);
            
            Assert.False(parseOk);
            Assert.Null(thingId);
        }
        
        [Fact]
        public void Parse_ValidIdString_ReturnsThingId()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";

            var thingId = ThingId.Parse($"{driver}:{gateway}:{thing}");
            
            Assert.Equal(driver, thingId.Driver);
            Assert.Equal(gateway, thingId.Gateway);
            Assert.Equal(thing, thingId.Thing);
        }
        
        [Fact]
        public void Parse_InvalidIdString_ThrowsException()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";

            Assert.Throws<FormatException>(() => ThingId.Parse($"{driver}:{gateway}:{thing}:1"));
        }
    }
}