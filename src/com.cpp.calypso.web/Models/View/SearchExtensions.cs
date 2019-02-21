using com.cpp.calypso.comun.dominio;
using System;
using System.Collections.Generic;

namespace com.cpp.calypso.web
{

    /// <summary>
    /// Extensiones para View Search
    /// </summary>
    public static class SearchExtensions
    {
        private static readonly Dictionary<Type, string> _defaultTemplateName =
            new Dictionary<Type, string>()
            {
                //{ "HiddenInput", DefaultEditorTemplates.HiddenInputTemplate },
                //{ "MultilineText", DefaultEditorTemplates.MultilineTextTemplate },
                //{ "Password", DefaultEditorTemplates.PasswordTemplate },
                //{ "Text", DefaultEditorTemplates.StringTemplate },
                //{ "Collection", DefaultEditorTemplates.CollectionTemplate },
                //{ "PhoneNumber", DefaultEditorTemplates.PhoneNumberInputTemplate },
                //{ "Url", DefaultEditorTemplates.UrlInputTemplate },
                //{ "EmailAddress", DefaultEditorTemplates.EmailAddressInputTemplate },
                //{ "DateTime", DefaultEditorTemplates.DateTimeInputTemplate },
                //{ "DateTime-local", DefaultEditorTemplates.DateTimeLocalInputTemplate },
                //{ "Date", DefaultEditorTemplates.DateInputTemplate },
                //{ "Time", DefaultEditorTemplates.TimeInputTemplate },
                { typeof(DateTime?), "Date"  },
                { typeof(DateTime), "Date"  },
                { typeof(byte),typeof(byte).Name},
                { typeof(sbyte),typeof(sbyte).Name},
                { typeof(int), typeof(int).Name},
                { typeof(uint), typeof(uint).Name},
                { typeof(long), typeof(long).Name},
                { typeof(ulong), typeof(ulong).Name},
                { typeof(bool), typeof(bool).Name},
                { typeof(decimal), typeof(decimal).Name},
                { typeof(string), typeof(string).Name},
                { typeof(object), typeof(object).Name},
            };

        /// <summary>
        /// Obtener nombre de la plantilla, basado en el tipo del campo de busqueda
        /// </summary>
        /// <param name="fieldSearch"></param>
        /// <returns></returns>
        public static string GetTemplateNameForType(this FieldSearch fieldSearch)
        {
            string result;

            _defaultTemplateName.TryGetValue(fieldSearch.FieldType, out  result);
            return result;
        }


    }
}