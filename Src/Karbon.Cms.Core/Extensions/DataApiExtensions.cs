using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karbon.Cms.Core
{
    public static class DataApiExtensions
    {
        /// <summary>
        /// Gets the value for the given key.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static string GetValue(this IDictionary<string, string> data, string key, string defaultValue = "")
        {
            string value;
            return data.TryGetValue(key, out value) && !string.IsNullOrEmpty(value)
                ? value
                : defaultValue;
        }

        /// <summary>
        /// Gets the value for the given key.
        /// </summary>
        /// <typeparam name="TValueType">The type of the value type.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static TValueType GetValue<TValueType>(this IDictionary<string, string> data, string key, TValueType defaultValue = default(TValueType))
        {
            TValueType value;
            return data.TryGetValue(key, out value) && !EqualityComparer<TValueType>.Default.Equals(value, default(TValueType))
                ? value
                : defaultValue;
        }

        /// <summary>
        /// Gets the value for the given key.
        /// </summary>
        /// <typeparam name="TValueType">The type of the value type.</typeparam>
        /// <typeparam name="TConverterType">The type of the converter type.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static TValueType GetValue<TValueType, TConverterType>(this IDictionary<string, string> data, string key, TValueType defaultValue = default(TValueType))
            where TConverterType : TypeConverter
        {
            TValueType value;
            return data.TryGetValue<TValueType, TConverterType>(key, out value) && !EqualityComparer<TValueType>.Default.Equals(value, default(TValueType))
                ? value
                : defaultValue;
        }

        /// <summary>
        /// Tries to get the value for the given key.
        /// </summary>
        /// <typeparam name="TValueType">The type of the value type.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool TryGetValue<TValueType>(this IDictionary<string, string> data, string key, out TValueType value)
        {
            value = default(TValueType);

            if (!data.ContainsKey(key))
                return false;

            try
            {
                var tmpValue = data[key];
                var converter = TypeDescriptor.GetConverter(typeof(TValueType));
                value = (TValueType)converter.ConvertFromString(tmpValue);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Tries to get the value for the given key.
        /// </summary>
        /// <typeparam name="TValueType">The type of the value type.</typeparam>
        /// <typeparam name="TConverterType">The type of the converter type.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool TryGetValue<TValueType, TConverterType>(this IDictionary<string, string> data, string key, out TValueType value)
            where TConverterType : TypeConverter
        {
            value = default(TValueType);

            if (!data.ContainsKey(key))
                return false;

            try
            {
                var tmpValue = data[key];
                var converter = Activator.CreateInstance(typeof (TConverterType)) as TypeConverter;
                if (converter == null)
                    return false;

                value = (TValueType)converter.ConvertFromString(tmpValue);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
