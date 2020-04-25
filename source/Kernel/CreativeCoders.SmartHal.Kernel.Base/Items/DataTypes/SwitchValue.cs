using System;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Items.DataTypes
{
    [PublicAPI]
    public class SwitchValue
    {
        private readonly double _value;

        private readonly Func<double, bool> _equalFunc;

        public SwitchValue(double value, Func<double, bool> equalFunc)
        {
            _value = value;
            _equalFunc = equalFunc;
        }

        public override bool Equals(object obj)
        {
            var switchValue = this;

            return obj switch
            {
                SwitchValue switchValue1 => Math.Abs(switchValue._value - switchValue1._value) < 0.0001,
                int intValue => _equalFunc(intValue),
                double doubleValue => _equalFunc(doubleValue),
                bool boolValue => _equalFunc(boolValue ? 1 : 0),
                _ => false
            };
        }

        public override string ToString()
        {
            if (this == Switch.On)
            {
                return "Switch.On";
            }
            
            return this == Switch.Off ? "Switch.Off" : $"SwitchValue({_value})";
        }

        protected bool Equals(SwitchValue other) => _value.Equals(other._value);

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(SwitchValue switchValue, object value1)
        {
            return !ReferenceEquals(switchValue, null) && switchValue.Equals(value1);
        }

        public static bool operator !=(SwitchValue switchValue, object value1)
        {
            return ReferenceEquals(switchValue, null) || !switchValue.Equals(value1);
        }

        public static implicit operator double(SwitchValue switchValue)
        {
            return switchValue._value;
        }
    }
}