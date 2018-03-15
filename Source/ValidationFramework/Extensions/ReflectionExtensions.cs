using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// Reflection extension methods.
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// The propertiy information cache.
        /// </summary>
        private static Dictionary<Type, Dictionary<string, PropertyData>> typeProperties = new Dictionary<Type, Dictionary<string, PropertyData>>();

        /// <summary>
        /// Gets the properties for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The property information.</returns>
        public static Dictionary<string, PropertyData> GetProperties(this Type type)
        {
            // TODO: should we include fields as well?
            BindingFlags bindings = BindingFlags.Public | BindingFlags.Instance;

            if (!typeProperties.TryGetValue(type, out Dictionary<string, PropertyData> properties))
            {
                properties = new Dictionary<string, PropertyData>();

                // get property information
                foreach (var propertyInfo in type.GetProperties(bindings))
                {
                    properties.Add(propertyInfo.Name, new PropertyData(propertyInfo, propertyInfo.GetCustomAttributes<ValidationAttribute>(true).Cast<ValidationAttribute>().OrderByDescending(x => x.ValidationPriority).ToList()));
                }

                // cache property information
                typeProperties.Add(type, properties);
            }

            return properties;
        }
    }
}
