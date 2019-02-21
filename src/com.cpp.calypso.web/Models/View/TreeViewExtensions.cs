using System;
using System.Collections.Generic;
using System.Reflection;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;

namespace com.cpp.calypso.web
{
    public static class TreeViewExtensions
    {


        public static object GetValue(this Field field, object model)
        {
            PropertyInfo property = model.GetType().GetProperty(field.Name);
            if (property == null)
            {
                var msg = string.Format("La propiedad [{0}] no existe en el modelo de tipo [{1}]",
                    field.Name, model.GetType());
                throw new ArgumentException(msg);
            }

            var value = property.GetValue(model, null);

            return value;
        }

        public static List<PropertyInfo> GetPropertiesFilter(this Tree view, Type model)
        {

            //TODO: ADD Cache 

            //Model
            PropertyInfo[]
                properties = model.GetEnumerableType().GetProperties();

            List<PropertyInfo> listFilter = new List<PropertyInfo>();

            foreach (var item in view.Fields)
            {
                //item.Name
                foreach (var pro in properties)
                {

                    if (item.Name == pro.Name)
                    {
                        listFilter.Add(pro);
                    }
                }
            }
            return listFilter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public static IList<string> GetDescriptionProperties(this Tree view, List<PropertyInfo> properties)
        {
 
            var headerList = new List<string>();
 
            foreach (var pro in properties)
            {
               
                headerList.Add(pro.GetDescription());
            }

            return headerList;
        }

        
    }
}