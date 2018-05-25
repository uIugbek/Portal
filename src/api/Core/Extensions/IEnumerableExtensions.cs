using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Portal.Apis.Core.Configuration;

namespace Portal.Apis.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable IsNotDeleted(this IEnumerable query)
        {
            var elementType = GetEnumerableElementType(query.GetType());
            if (elementType.GetProperty(Settings.IS_DELETED_PROPERTY) == null)
                foreach (var item in query)
                    yield return item;
            else
            {
                var a = Expression.Parameter(elementType, "a");
                var prop = Expression.Property(a, Settings.IS_DELETED_PROPERTY);
                var value = Expression.Constant(false);

                var isNotDeleted = Expression.Lambda(Expression.Equal(prop, value), a).Compile();
                foreach (var item in query)
                {
                    if ((bool)isNotDeleted.DynamicInvoke(item))
                        yield return item;
                }
            }
            yield break;
        }

        public static Type GetEnumerableElementType(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                return type.GetGenericArguments()[0];

            var iface = (from i in type.GetInterfaces()
                         where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                         select i).FirstOrDefault();

            if (iface == null)
                throw new ArgumentException("Does not represent an enumerable type.", "type");

            return GetEnumerableElementType(iface);
        }

    }
}