using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Portal.Apis.Core.Attributes;

namespace Portal.Apis.Core.Extensions
{
    public static class PropertyExtension
    {
        private static object _lock = new object();
        public static string GenerateKey(this PropertyInfo property, Type entType, int id)
        {
            var key = new StringBuilder();

            lock (_lock)
            {
                key.Append(entType.Name);
                key.Append(property.Name);
                key.Append(DateTime.Now.ToFileTime());
            }
            
            return key.ToString();
        }

        public static bool IsLocalizedProperty(this PropertyInfo property)
        {
            return !property.PropertyType.IsGenericType && 
                    typeof(string).IsAssignableFrom(property.PropertyType) &&
                    property.GetCustomAttributes(typeof(LocalizedAttribute), false).Any();
        }

        public static bool IsStringProperty(this PropertyInfo property)
        {
            return !property.PropertyType.IsGenericType && 
                    typeof(string).IsAssignableFrom(property.PropertyType);
        }
    }
}