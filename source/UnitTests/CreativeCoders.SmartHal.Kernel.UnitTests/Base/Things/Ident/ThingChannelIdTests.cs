using System;
using CreativeCoders.SmartHal.Kernel.Base.Things.Ident;
using Xunit;

namespace CreativeCoders.SmartHal.Kernel.UnitTests.Base.Things.Ident
{
    public class ThingChannelIdTests
    {
        [Fact]
        public void ToString_CreateId_ResultOk()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string channel = "Channel1";
            
            var thingChannelId = new ThingChannelId(new ThingId(new GatewayId(driver, gateway), thing), channel);
            
            Assert.Equal($"{driver}:{gateway}:{thing}:{channel}", thingChannelId.ToString());
        }
        
        [Fact]
        public void Channel_CreateId_GetValueOk()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string channel = "Channel1";
            
            var thingChannelId = new ThingChannelId(new ThingId(new GatewayId(driver, gateway), thing), channel);
            
            Assert.Equal(thing, thingChannelId.Thing);
        }
        
        [Fact]
        public void Thing_CreateId_GetValueOk()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string channel = "Channel1";
            
            var thingChannelId = new ThingChannelId(new ThingId(new GatewayId(driver, gateway), thing), channel);
            
            Assert.Equal(thing, thingChannelId.Thing);
        }
        
        [Fact]
        public void Gateway_CreateId_GetValueOk()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string channel = "Channel1";
            
            var thingChannelId = new ThingChannelId(new ThingId(new GatewayId(driver, gateway), thing), channel);
            
            Assert.Equal(gateway, thingChannelId.Gateway);
        }
        
        [Fact]
        public void Driver_CreateId_GetValueOk()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string channel = "Channel1";
            
            var thingChannelId = new ThingChannelId(new ThingId(new GatewayId(driver, gateway), thing), channel);
            
            Assert.Equal(driver, thingChannelId.Driver);
        }

        [Fact]
        public void Equals_SameIdString_ReturnsTrue()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string channel = "Channel1";
            
            var thingChannelId = new ThingChannelId(new ThingId(new GatewayId(driver, gateway), thing), channel);
            
            Assert.True(thingChannelId.Equals($"{driver}:{gateway}:{thing}:{channel}"));
        }
        
        [Fact]
        public void Equals_NotSameIdString_ReturnsFalse()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string channel = "Channel1";
            const string driver2 = "Driver2";
            
            var thingChannelId = new ThingChannelId(new ThingId(new GatewayId(driver, gateway), thing), channel);
            
            Assert.False(thingChannelId.Equals($"{driver2}:{gateway}:{thing}:{channel}"));
        }
        
        [Fact]
        public void Equals_SameId_ReturnsTrue()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string channel = "Channel1";
            
            var thingChannelId = new ThingChannelId(new ThingId(new GatewayId(driver, gateway), thing), channel);
            
            Assert.True(thingChannelId.Equals(thingChannelId));
        }
        
        [Fact]
        public void Equals_OtherIdWithSameValue_ReturnsTrue()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string channel = "Channel1";
            
            var thingChannelId = new ThingChannelId(new ThingId(new GatewayId(driver, gateway), thing), channel);
            var thingChannelId2 = new ThingChannelId(new ThingId(new GatewayId(driver, gateway), thing), channel);
            
            Assert.True(thingChannelId.Equals(thingChannelId2));
        }
        
        [Fact]
        public void Equals_NullId_ReturnsFalse()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string channel = "Channel1";
            
            var thingChannelId = new ThingChannelId(new ThingId(new GatewayId(driver, gateway), thing), channel);

            Assert.False(thingChannelId.Equals((IdBase) null));
        }
        
        [Fact]
        public void Equals_SameIdStringObject_ReturnsTrue()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string channel = "Channel1";
            
            var thingChannelId = new ThingChannelId(new ThingId(new GatewayId(driver, gateway), thing), channel);

            object id = $"{driver}:{gateway}:{thing}:{channel}";

            Assert.True(thingChannelId.Equals(id));
        }
        
        [Fact]
        public void Equals_NotSameIdStringObject_ReturnsFalse()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string channel = "Channel1";
            const string driver2 = "Driver2";
            
            var thingChannelId = new ThingChannelId(new ThingId(new GatewayId(driver, gateway), thing), channel);
            
            object id = $"{driver2}:{gateway}:{thing}:{channel}";
            
            Assert.False(thingChannelId.Equals(id));
        }
        
        [Fact]
        public void Equals_SameIdObject_ReturnsTrue()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string channel = "Channel1";
            
            var thingChannelId = new ThingChannelId(new ThingId(new GatewayId(driver, gateway), thing), channel);

            object id = thingChannelId; 
            
            Assert.True(thingChannelId.Equals(id));
        }
        
        [Fact]
        public void Equals_OtherIdWithSameValueObject_ReturnsTrue()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string channel = "Channel1";
            
            var thingChannelId = new ThingChannelId(new ThingId(new GatewayId(driver, gateway), thing), channel);
            var thingChannelId2 = new ThingChannelId(new ThingId(new GatewayId(driver, gateway), thing), channel);

            object id = thingChannelId2; 
            
            Assert.True(thingChannelId.Equals(id));
        }

        [Fact]
        public void GetHashCode_ThingChannelId_EqualsIdStringHashCode()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string channel = "Channel1";
            
            var thingChannelId = new ThingChannelId(new ThingId(new GatewayId(driver, gateway), thing), channel);
            
            Assert.Equal(thingChannelId.ToString().GetHashCode(), thingChannelId.GetHashCode());
        }

        [Fact]
        public void TryParse_ValidIdString_ReturnsTrueAndOutThingChannelId()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string channel = "Channel1";

            var parseOk = ThingChannelId.TryParse($"{driver}:{gateway}:{thing}:{channel}", out var thingChannelId);
            
            Assert.True(parseOk);
            Assert.Equal(driver, thingChannelId.Driver);
            Assert.Equal(gateway, thingChannelId.Gateway);
            Assert.Equal(thing, thingChannelId.Thing);
            Assert.Equal(channel, thingChannelId.Channel);
        }
        
        [Fact]
        public void TryParse_InvalidIdString_ReturnsFalseAndThingChannelIdNull()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string channel = "Channel1";

            var parseOk = ThingChannelId.TryParse($"{driver}:{gateway}:{thing}:{channel}:1", out var thingChannelId);
            
            Assert.False(parseOk);
            Assert.Null(thingChannelId);
        }
        
        [Fact]
        public void Parse_ValidIdString_ReturnsThingChannelId()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string channel = "Channel1";

            var thingChannelId = ThingChannelId.Parse($"{driver}:{gateway}:{thing}:{channel}");
            
            Assert.Equal(driver, thingChannelId.Driver);
            Assert.Equal(gateway, thingChannelId.Gateway);
            Assert.Equal(thing, thingChannelId.Thing);
            Assert.Equal(channel, thingChannelId.Channel);
        }
        
        [Fact]
        public void Parse_InvalidIdString_ThrowsException()
        {
            const string driver = "Driver1";
            const string gateway = "Gateway1";
            const string thing = "Thing1";
            const string channel = "Channel1";

            Assert.Throws<FormatException>(() => ThingChannelId.Parse($"{driver}:{gateway}:{thing}:{channel}:1"));
        }
    }
}