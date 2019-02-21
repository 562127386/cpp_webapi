using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace com.cpp.calypso.comun.dominio
{
    public static class PropertyInfoExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static string GetDescription(this PropertyInfo pro)
        {

            var descriptionAttribute = pro.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            var header = string.Empty;
            if (descriptionAttribute != null)
            {
                header = descriptionAttribute.Description;
            }
            else
            {
                var displayNameAttribute =
                    pro.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault() as
                    DisplayNameAttribute;
                if (displayNameAttribute != null)
                {
                    header = displayNameAttribute.DisplayName;
                }
                else
                {
                    header = pro.Name.Replace('_', ' ');
                }
            }

            return header;
        }
    }
}
