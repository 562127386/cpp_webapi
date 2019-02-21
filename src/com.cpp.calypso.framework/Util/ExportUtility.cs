using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace com.cpp.calypso.framework
{
    public static class ExportUtility
    {

        /// <summary>
        /// Exportar a excel
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ExportExcel<TEntity>(IList<TEntity> data, MemberInfo[] members)
        {
            var ms = new MemoryStream();

            //TODO: EPPlus utiliza DescriptionAttribute de las propiedades para establecer los nombres de las columnas
            //se deberia adaptar para soportar el valor de "String" del campo en las vistas 
            //Opcion 2. Cambiar los valores de las celdas de la fila 0, y la colunma correspondiente
            //y establecer el valor del header, directamente... pasado como parametro desde el modelo...
            //ws.Cells[0,col_propiedad].Value = header (Obtener desde modelo)

            //var type = typeof(TEntity);
            //foreach (var t in members)
            //{
            //    if (t.DeclaringType != null && t.DeclaringType != type && !t.DeclaringType.IsSubclassOf(type))
            //    {
            //        //throw new InvalidCastException("Supplied properties in parameter Properties must be of the same type as T (or an assignable type from T");
            //        var error = "Supplied properties in parameter Properties must be...";
            //    }
            //}


            BindingFlags memberFlags = BindingFlags.Public | BindingFlags.Instance;

            using (var xlPackage = new ExcelPackage(ms))
            {

                var wb = xlPackage.Workbook;
                var ws = wb.Worksheets.Add("Datos");
                ws.Cells.LoadFromCollection<TEntity>(data, true, TableStyles.None, memberFlags, members);



                //Apply Styles 
                //TODO: Campusoft. Revisar si se puede aplicar cache
                PropertyInfo[] propertyInfos = typeof(TEntity).GetProperties(memberFlags);

                //TODO: Utilizar metodo GetMemberType, para no volver a recuperar las propiedades del objecto, sino trabajar con 
                //el parametro MemberInfo[] members directamente.
                int col = 1;
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    if (members.Any(val => propertyInfo.Name.ToUpper() == val.Name.ToUpper()))
                    {
                        //DateTime
                        if (propertyInfo.PropertyType == typeof(DateTime) ||
                            propertyInfo.PropertyType == typeof(DateTime?))
                        {
                            ws.Column(col).Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                           
                        }
                        col++;
                    }
                }


                xlPackage.Save();
                return ms.ToArray();
            }
        }

        private static Type GetMemberType(MemberInfo memberInfo)
        {
            if (memberInfo.MemberType == MemberTypes.Field)
            {
                return ((FieldInfo)memberInfo).FieldType;
            }
            if (memberInfo.MemberType == MemberTypes.Property)
            {
                return ((PropertyInfo)memberInfo).PropertyType;
            }

            throw new NotSupportedException(string.Format("Does not support the member type {0}", memberInfo.MemberType));
        }

    }
}
