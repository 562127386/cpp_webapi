using com.cpp.calypso.comun.dominio;
using CommonServiceLocator;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace  com.cpp.calypso.web
{
    /// <summary>
    /// Filtro para validar si el usuario se encuentra autorizado
    /// </summary>
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            HttpContext ctx = HttpContext.Current;

            // Verifica si la session es soportada
            if (ctx.Session != null)
            {

                bool isAllowAnonymousAttribute = filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);

                if (!isAllowAnonymousAttribute)
                {

                    var app = ServiceLocator.Current.GetInstance<IApplication>();
                    if (!app.IsAuthenticated())
                    {

                        //Asegurar eliminar cualquier rastro de autentificacion
                        if (ctx.Request.IsAuthenticated)
                        {
                            ctx.Session.Clear();
                            var authenticationManager = ServiceLocator.Current.GetInstance<IAuthenticationManager>();
                            if (authenticationManager != null) {

                                authenticationManager.SignOut();
                            }

                        }

                        //TODO: Use Owin  LoginPath
                        ctx.Response.Redirect(Constantes.AUTENTIFICACION_LOGIN);
 
                    }
                }

            }
            base.OnActionExecuting(filterContext);
        }
    }
}