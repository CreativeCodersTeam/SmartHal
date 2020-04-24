using System;
using System.Collections.Generic;
using System.Globalization;
using CreativeCoders.SmartHal.Config.Base;

namespace CreativeCoders.SmartHal.Config.FileSystem
{
    public abstract class ConfigurationObjectSettingsBase : ConfigurationObjectBase, IConfigurationObjectSettings
    {
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

        public IDictionary<string, object> Settings { get; } = new Dictionary<string, object>();
    }
}