using System;
using System.Collections.Generic;
using System.Globalization;

namespace CreativeCoders.SmartHal.Kernel.SubSystems.Things.Building.SetupInfos
{
    public abstract class SetupInfoBase
    {
        protected SetupInfoBase(IDictionary<string, object> settings)
        {
            Settings = settings;
        }
        
        public T ReadSetting<T>(string name)
        {
            return ReadSetting(name, default(T));
        }

        public T ReadSetting<T>(string name, T defaultValue)
        {
            if (!Settings.TryGetValue(name, out var value))
            {
                return defaultValue;
            }

            if (value is T castedValue)
            {
                return castedValue;
            }

            try
            {
                var convertedValue = Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
                return (T) convertedValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public IDictionary<string, object> Settings { get; }
    }
}