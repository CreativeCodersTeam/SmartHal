using System;
using CreativeCoders.SmartHal.Kernel.Base.Items.DataTypes;
using Xunit;
using Xunit.Sdk;

namespace CreativeCoders.SmartHal.Kernel.UnitTests.Items.DataTypes
{
    public class SwitchValueTests
    {
        [Fact]
        public void Equals_SwitchValueWithEqualValue_ReturnsTrue()
        {
            var switchValue = new SwitchValue(1, _ => throw new XunitException());

            var switchValue2 = new SwitchValue(1, x => false);
            var objectValue = (object) switchValue2;

            Assert.True(switchValue.Equals(objectValue));
        }
        
        [Fact]
        public void Equals_SwitchValueWithNotEqualValue_ReturnsFalse()
        {
            var switchValue = new SwitchValue(1, _ => throw new XunitException());

            var switchValue2 = new SwitchValue(0.5, x => false);
            var objectValue = (object) switchValue2;

            Assert.False(switchValue.Equals(objectValue));
        }
        
        [Fact]
        public void Equals_IntValue_EqualsFuncCalled()
        {
            double valuePassed = 0;
            
            var switchValue = new SwitchValue(1, x =>
            {
                valuePassed = x;
                return true;
            });

            const int value = 1;
            var objectValue = (object) value;

            Assert.True(switchValue.Equals(objectValue));
            Assert.True(Math.Abs(valuePassed - value) < 0.0001);
        }
        
        [Fact]
        public void Equals_DoubleValue_EqualsFuncCalled()
        {
            double valuePassed = 0;
            
            var switchValue = new SwitchValue(1, x =>
            {
                valuePassed = x;
                return true;
            });

            const double value = 1;
            var objectValue = (object) value;

            Assert.True(switchValue.Equals(objectValue));
            Assert.True(Math.Abs(valuePassed - value) < 0.0001);
        }
        
        [Fact]
        public void Equals_BoolValueTrue_OnePassedToEqualsFunc()
        {
            double valuePassed = 0;
            
            var switchValue = new SwitchValue(1, x =>
            {
                valuePassed = x;
                return true;
            });

            const double value = 1;
            var objectValue = (object) true;

            Assert.True(switchValue.Equals(objectValue));
            Assert.True(Math.Abs(valuePassed - value) < 0.0001);
        }
        
        [Fact]
        public void Equals_BoolValueFalse_ZeroPassedToEqualsFunc()
        {
            double valuePassed = 0;
            
            var switchValue = new SwitchValue(1, x =>
            {
                valuePassed = x;
                return true;
            });

            const double value = 0;
            var objectValue = (object) false;

            Assert.True(switchValue.Equals(objectValue));
            Assert.True(Math.Abs(valuePassed - value) < 0.0001);
        }
        
        [Fact]
        public void Equals_OtherType_ReturnsFalse()
        {
            var switchValue = new SwitchValue(1, _ => throw new XunitException());

            const string value = "1";
            var objectValue = (object) value;

            Assert.False(switchValue.Equals(objectValue));
        }
    }
}