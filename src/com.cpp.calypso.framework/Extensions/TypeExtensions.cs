using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace com.cpp.calypso.framework
{
    public static class TypeExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static string GetDescription(this Type type)
        {
            if (type == null)
                return string.Empty;

            var descriptionAttribute = type.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            var header = string.Empty;
            if (descriptionAttribute != null)
            {
                header = descriptionAttribute.Description;
            }
            else
            {
                var displayNameAttribute =
                    type.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault() as
                    DisplayNameAttribute;
                if (displayNameAttribute != null)
                {
                    header = displayNameAttribute.DisplayName;
                }
                else
                {
                    if (type.BaseType != null && type.Namespace == "System.Data.Entity.DynamicProxies")
                    {
                        header = type.BaseType.Name.Replace('_', ' ');
                    }
                    else {
                        header = type.Name.Replace('_', ' ');
                    }          
                }
            }

            return header;
        }

        public static bool Implements<TInterface>(this Type type)
        {
            var interfaceType = typeof(TInterface);
            return Implements(type, interfaceType);
        }

        public static bool Implements(this Type type, Type interfaceType)
        {
            return type.GetInterface(interfaceType.Name) != null;
        }

        /// <summary>
        /// Obtener Propiedades Publicas de un tipo, incluidas las heredadas 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static PropertyInfo[] GetPublicProperties(this Type type)
        {
            if (type.IsInterface)
            {
                var propertyInfos = new List<PropertyInfo>();

                var considered = new List<Type>();
                var queue = new Queue<Type>();
                considered.Add(type);
                queue.Enqueue(type);
                while (queue.Count > 0)
                {
                    var subType = queue.Dequeue();
                    foreach (var subInterface in subType.GetInterfaces())
                    {
                        if (considered.Contains(subInterface)) continue;

                        considered.Add(subInterface);
                        queue.Enqueue(subInterface);
                    }

                    var typeProperties = subType.GetProperties(
                        BindingFlags.FlattenHierarchy
                        | BindingFlags.Public
                        | BindingFlags.Instance);

                    var newPropertyInfos = typeProperties
                        .Where(x => !propertyInfos.Contains(x));

                    propertyInfos.InsertRange(0, newPropertyInfos);
                }

                return propertyInfos.ToArray();
            }

            return type.GetProperties(BindingFlags.FlattenHierarchy
                | BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// Get the type of Enumerable 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetEnumerableType(this Type type)
        {
            var interfaceTypes = type.GetInterfaces().ToList();
            interfaceTypes.Insert(0, type);
            foreach (Type intType in interfaceTypes)
            {
                if (intType.IsGenericType
                    && intType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return intType.GetGenericArguments()[0];
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        //public static bool IsNullableEnum(this Type t)
        //{
        //    Type u = Nullable.GetUnderlyingType(t);
        //    return (u != null) && u.IsEnum;
        //}
    }
}