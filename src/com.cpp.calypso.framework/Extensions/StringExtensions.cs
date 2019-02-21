using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Extensiones para trabajar con cadenas
    /// </summary>
    public static class StringExtensions
    {

        public static bool IsMatch(this string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
        }

        public static string RemoveAtEnd(this string source, string value)
        {
            return source.Remove(source.Length - value.Length);
        }

        public static bool IsPlural(this string value)
        {
            //TODO:JGA Hacer algo más complejo y exacto, para eso, ver:
            // http://fabiomaulo.blogspot.com/2009/06/spanish-inflectornet.html
            // http://www.elponeypisador.com/Foro/viewtopic.php?t=1147
            return value.EndsWith("s");
        }

        public static string ToSingular(this string value)
        {
            //TODO:JGA Hacer algo más complejo y exacto, para eso, ver IsPlural
            return value.RemoveAtEnd("s");
        }
    }
}
