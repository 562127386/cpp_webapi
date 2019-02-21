using CommonServiceLocator;
using System;
using System.Web;
using System.Web.Mvc;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Filtro para manejar las excepciones, en el caso de ser ajax se pasa un json con la información del error
    /// </summary>
    public class HandleErrorFilterAttribute : HandleErrorAttribute
    {
        //private static ErrorFilterConfiguration _config;

        public override void OnException(ExceptionContext filterContext)
        {
            //Referencia del Codigo:
            //https://github.com/ASP-NET-MVC/aspnetwebstack/blob/master/src/System.Web.Mvc/HandleErrorAttribute.cs


            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }



            //Registrar exception con el manejador de excepciones configurado
            var manejadorExcepciones = ServiceLocator.Current.GetInstance<IHandlerExcepciones>();
            var _exceptionResult = manejadorExcepciones.HandleException(filterContext.Exception);



            if (filterContext.IsChildAction)
            {
                return;
            }



            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            //TODO: Si el error es diferente de 500.. 
            //que pasa cuando son llamas ajax ???
            if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            {
                return;
            }

            if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
            {
                return;
            }

            // if the request is AJAX return JSON else view.
            //if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                /*
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        error = true,
                        message = filterContext.Exception.Message
                    }
                };
                */

                var result = new JsonResult
                {
                    Data = new { error = _exceptionResult.Message },
                    //ContentType = contentType,
                    //ContentEncoding = contentEncoding,
                    JsonRequestBehavior = JsonRequestBehavior.DenyGet
                };

                //result.
                filterContext.Result = result;

            }
            else
            {
                var controllerName = (string)filterContext.RouteData.Values["controller"];
                var actionName = (string)filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

                filterContext.Result = new ViewResult
                {
                    ViewName = View,
                    MasterName = Master,
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = filterContext.Controller.TempData
                };

            }



            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;

            // Certain versions of IIS will sometimes use their own error page when
            // they detect a server error. Setting this property indicates that we
            // want it to try to render ASP.NET MVC's error page instead.
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

        }
    }
}
