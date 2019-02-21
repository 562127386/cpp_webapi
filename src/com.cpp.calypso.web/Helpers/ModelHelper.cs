using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace com.cpp.calypso.web
{
    public static class ModelHelper
    {
        public static string GetFullName(string firstName, string lastName, string id = null)
        {
            firstName = (firstName ?? String.Empty).Trim();
            lastName = (lastName ?? String.Empty).Trim();

            string str1 = lastName, str2 = firstName;

            var sb = new StringBuilder(str1);
            if (str2 != String.Empty)
            {
                if (sb.Length > 0)
                {
                    sb.Append(" ");
                }
                sb.Append(str2);
            }
            if (!String.IsNullOrEmpty(id))
            {
                string format = sb.Length > 0 ? " ({0})" : "{0}";
                sb.AppendFormat(format, id);
            }

            return sb.ToString();
        }

        public static string GetModulo(TimeSpan inicio, TimeSpan fin)
        {
            return String.Format("{0:hh\\:mm}-{1:hh\\:mm}", inicio, fin);
        }

        public static SelectList ToSelectList(this IEnumerable<Nomenclature> items)
        {
            if (items == null) throw new ArgumentNullException("items");
            return new SelectList(items, "Value", "Text");
        }
    }
}