using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace com.cpp.calypso.framework
{
    ///// <summary>
    ///// Filtro para validar si el usuario se encuentra autorizado
    ///// </summary>
    //public class SessionExpireFilterAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {

    //        HttpContext ctx = HttpContext.Current;

    //        // Verifica si la session es soportada
    //        if (ctx.Session != null)
    //        {

    //            bool isAllowAnonymousAttribute = filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);

    //            if (!isAllowAnonymousAttribute)
    //            {

    //                //TODO: Colocar la logica dentro del objeto del sistema ISyllabusApplication
    //                // Verificar si existe los valores para verificar si el usuario esta autorizacion. 
    //                //NSGA, crea una session a nivel de datos, dicho dato se guarda en HttpContext.Current.Session[ConstantesFramework.SESIONID]
    //                //ADD. Verificar si existe ROL asociado

    //                //if (HttpContext.Current.Session[ConstantesFramework.SESIONID] == null
    //                //    || HttpContext.Current.Session[ConstantesFramework.SESIONCREADA] == null ||
    //                //        HttpContext.Current.Session[ConstantesFramework.ROL] == null)
    //                //{

    //                //    if (ctx.Request.IsAuthenticated)
    //                //    {
    //                //        ctx.Session.Clear();
    //                //        FormsAuthentication.SignOut();
    //                //    }
    //                //    ctx.Response.Redirect(FormsAuthentication.LoginUrl);
    //                //}
    //            }

    //        }
    //        base.OnActionExecuting(filterContext);
    //    }
    //}
}
