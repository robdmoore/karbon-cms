using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Core.Mapping
{
    public class DataMapper
    {
        /// <summary>
        /// Maps the specified data an entity.
        /// </summary>
        /// <typeparam name="TEntityType">The entity type.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public TEntityType Map<TEntityType>(TEntityType entity, IDictionary<string, string> data)
            where TEntityType : Entity
        {
            var baseType = typeof (Entity);
            var baseProps = baseType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            
            var type = typeof (TEntityType);
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => baseProps.All(y => y.Name != x.Name));

            foreach (var dataKey in data.Keys)
            {
                var prop = props.SingleOrDefault(x => x.Name == dataKey && x.CanWrite);
                if (prop != null)
                {
                    // Found property, so attempt to set it
                    TypeConverter typeConverter = null;

                    var typeConverterAttr = prop.GetCustomAttribute<TypeConverterAttribute>();
                    if(typeConverterAttr != null)
                    {
                        var typeConverterType = Type.GetType(typeConverterAttr.ConverterTypeName);
                        if (typeConverterType != null)
                            typeConverter = Activator.CreateInstance(typeConverterType) as TypeConverter;
                    }

                    if (typeConverter == null)
                        typeConverter = TypeDescriptor.GetConverter(prop.PropertyType);

                    var propValue = typeConverter.ConvertFromString(data[dataKey]);
                    prop.SetValue(entity, propValue);
                }
                else
                {
                    // No property so add to data collection
                    if (entity.Data.ContainsKey(dataKey))
                        entity.Data[dataKey] = data[dataKey];
                    else
                        entity.Data.Add(dataKey, data[dataKey]);
                }
            }

            return entity;
        }
    }
}
