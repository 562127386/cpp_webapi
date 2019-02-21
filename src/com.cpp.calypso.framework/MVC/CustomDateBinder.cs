using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Binder para trabajar con fechas
    /// </summary>
    public class CustomDateBinder : IModelBinder
    {
        /// <summary>
        /// Variable paraa realizar Log
        /// </summary>
        static ILogger log = null;
        public CustomDateBinder(ILoggerFactory loggerFactory) {

            log = loggerFactory.Create(typeof(CustomDateBinder));
        }

       
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext", "controllerContext is null.");
            if (bindingContext == null)
                throw new ArgumentNullException("bindingContext", "bindingContext is null.");

            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (value == null)
                throw new ArgumentNullException(bindingContext.ModelName);

            CultureInfo cultureInf = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            //cultureInf.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            log.DebugFormat("CultureInfo: {0}", cultureInf.Name);
            log.DebugFormat("DateTimeFormat.ShortDatePattern: {0}", cultureInf.DateTimeFormat.ShortDatePattern);
            log.DebugFormat("DateTimeFormat.ShortTimePattern: {0}", cultureInf.DateTimeFormat.ShortTimePattern);


            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);

            log.DebugFormat("value.Culture.Name: {0}", value.Culture.Name);
            log.DebugFormat("value.RawValue: {0}", value.RawValue);

            try
            {
                var date = value.ConvertTo(typeof(DateTime), cultureInf);

                return date;
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex);
                return null;
            }
        }


    }
}
