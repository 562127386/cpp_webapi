//using CommonServiceLocator;
//using System;
//using System.Net;
//using System.Net.Http;

//namespace com.cpp.calypso.framework
//{
//    /// <summary>
//    /// Filtro para manejar las excepciones  web api
//    /// </summary>
//    public class HandleErrorFilterApiAttribute : ExceptionFilterAttribute
//    {
//        public override void OnException(HttpActionExecutedContext filterContext)
//        {
//            //Referencia del Codigo:
//            //https://github.com/ASP-NET-MVC/aspnetwebstack/blob/master/src/System.Web.Mvc/HandleErrorAttribute.cs


//            if (filterContext == null)
//            {
//                throw new ArgumentNullException("filterContext");
//            }
 

//            //Registrar exception con el manejador de excepciones configurado
//            var manejadorExcepciones = ServiceLocator.Current.GetInstance<IHandlerExcepciones>();
//            var _exceptionResult = manejadorExcepciones.HandleException(filterContext.Exception);

//            //return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
//            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
//            {
//                Content = new StringContent(_exceptionResult.Message),
//                ReasonPhrase = "Critical Exception"
//            });
                         
//        }

       
//    }
//}
