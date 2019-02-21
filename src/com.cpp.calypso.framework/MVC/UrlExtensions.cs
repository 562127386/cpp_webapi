using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.framework
{
    public static class UrlExtensions
    {
        /// <summary>
        /// Convierte las propiedades de un Objeto a una cadena con el formato QueryString
        /// </summary>
        /// <param name="request"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToQueryString(this object request, string separator = ",")
        {
            if (request == null)
                throw new ArgumentNullException("request");

            var result = new List<KeyValuePair<string, object>>();

            // Get all properties on the object
            var properties = request.GetType().GetProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            //Agregar todas las propiedades diferentes de enumables
            result.AddRange(properties.Where(
                  item => !(item.Value is IEnumerable) || (item.Value is string) 
                ).Select(item => new KeyValuePair<string,object>(item.Key,item.Value)));

            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType
                                        ? valueType.GetGenericArguments()[0]
                                        : valueType.GetElementType();

                if (valueElemType != null && (valueElemType.IsPrimitive || valueElemType == typeof(string)))
                {
                    var enumerable = properties[key] as IEnumerable;
                    ////properties[key] = string.Join(separator, enumerable.Cast<object>());
                    foreach (var item in enumerable)
                    {
                        result.Add(new KeyValuePair<string, object>(key, item));
                        //properties[key] = item;
                    }
                }
            }

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join("&", result
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value.ToString()))));
        }
    }
}
