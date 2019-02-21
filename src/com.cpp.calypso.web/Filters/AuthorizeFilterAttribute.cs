using Abp.Threading;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.seguridad.aplicacion;
using CommonServiceLocator;
using Microsoft.Owin.Security;
using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace com.cpp.calypso.web
{
    public class AuthorizeFilterAttribute : AuthorizeAttribute
    {

        public IAuthenticationManager AuthenticationManager
        {
            get;
            set;
        }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            //1. SISTEMA
            //   1.1 Funcionalidades
            //       1.1.1 Acciones
            //   1.2 Roles
            //       1.2.1 Permisos => Acciones => Funcionalidades
            //   1.3 Menus
            //      1.3.1. Items Menu => Funcionalidad

            //APLICAR AUTORIZACION BASADO EN EL NOMBRE DEL CONTROLADOR
            //controller/accion/parametros
            //{controller}/{action}/{id}
            //Mapear los nombres de los controllers a los menus y estos a las funcionalidades



            if (filterContext == null)
            {
                throw new ArgumentNullException("AuthorizeFilterAttribute");
            }


            HttpContext ctx = HttpContext.Current;
            if (!ctx.Request.IsAuthenticated)
                return;

            //1.  Verifica si la session es soportada y si el existe autentificacion activa
            if (ctx.Session != null)
            {
                var application = ServiceLocator.Current.GetInstance<IApplication>();

                //2.1 Verificar si existe autentificacion no activa en el sistema, y mvc tiene la peticion autentificada.
                //Existe una desface, se debe forzar a salir
                if (!application.IsAuthenticated())
                {
                    //TODO: JSA QUE PASA CON REQTICIONES AJAX ??? 
                    if (ctx.Request.IsAuthenticated)
                    {
                        ctx.Session.Clear();

                        var AuthenticationManager = ServiceLocator.Current.GetInstance<IAuthenticationManager>();
                        if (AuthenticationManager != null)
                        {
                            AuthenticationManager.SignOut();
                        }

                    }
                    ctx.Response.Redirect(Constantes.AUTENTIFICACION_LOGIN);

                    return;
                }
            }



            //1. Saltar autorizacion. 
            if (SecurityControllerHelper.SkipControllerActionSecurity(RepositoryAuthorizationFilter.Instance(), filterContext.ActionDescriptor))
            {
                //log.InfoFormat("No aplicar autorización a la action {0} del controller {1}", filterContext.ActionDescriptor.ActionName, filterContext.ActionDescriptor.ControllerDescriptor.ControllerName);
                return;
            }



            //TODO: JSA, PARA MEJORAR RENDIMIENTO.. SE DEBE GUARDAR 
            // (Controller/Action => Funcionalidad/accion Asociada), puesto que verifica esta combinacion por cada peticion al sistema. 

            var servicioFuncionalidad = ServiceLocator.Current.GetInstance<IFuncionalidadService>();

            var funcionalidadRelacionada = SecurityControllerHelper.ControllerToFunctionality(servicioFuncionalidad.GetFuncionalidades(),
                filterContext.ActionDescriptor.ControllerDescriptor);

            if (funcionalidadRelacionada == null)
            {
                var url = new UrlHelper(filterContext.RequestContext);
                var logonUrl = url.Action("NoAutorizado", "Acceso", new { area = "", reason = string.Format("No existe funcionalidades asociadas al controlador {0}", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName) });
                filterContext.Result = new RedirectResult(logonUrl);
                return;

            }
            else
            {
                var accion = SecurityControllerHelper.ActionControllerToActionFunctionality(RepositoryAuthorizationFilter.Instance(), funcionalidadRelacionada, filterContext.ActionDescriptor);

                var servicio = ServiceLocator.Current.GetInstance<IAuthorizationService>();

                var autorizado = AsyncHelper.RunSync(() => servicio.Authorize(accion));


                if (!autorizado)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                        var result = new JsonResult
                        {
                            Data = new { success = false, error = string.Format("Acceso restringido a la funcionalidad [{0}], en la acción de [{1}]", funcionalidadRelacionada.Nombre, accion.Nombre) },
                            //ContentType = contentType,
                            //ContentEncoding = contentEncoding,
                            JsonRequestBehavior = JsonRequestBehavior.DenyGet
                        };

                        filterContext.Result = result;
                        return;
                    }
                    else
                    {
                        //TODO:
                        var url = new UrlHelper(filterContext.RequestContext);
                        var logonUrl = url.Action("NoAutorizado", "Acceso", new { area = "", reason = string.Format("Acceso restringido a la funcionalidad {0}, en la acción de [{1}]", funcionalidadRelacionada.Nombre, accion.Nombre) });
                        filterContext.Result = new RedirectResult(logonUrl);
                        return;

                    }
                }

            }
        }
    }
}