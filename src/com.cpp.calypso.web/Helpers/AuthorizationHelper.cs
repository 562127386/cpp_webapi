using com.cpp.calypso.comun.dominio;
using System.Web.Mvc;

namespace com.cpp.calypso.web
{
    public static class AuthorizationHelper
    {
        #region MVC 

        public static bool CheckPermissions(this HtmlHelper html, string controllerName, string actionName)
        {
            return SecurityControllerHelper.CheckPermissions(controllerName, actionName);
        }

        /// <summary>
        /// Verificar permisos en un controlador y action formato
        /// controlador/action 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="controllerActionName">cadena con el formato controlador/action</param>
        /// <returns></returns>
        public static bool CheckPermissions(this HtmlHelper html, string controllerActionName)
        {
            var partes = controllerActionName.Split('/');
            if (partes.Length != 2) { 
                var msg = string.Format("El parametros {0}, debe tener un formato controlador/accion", controllerActionName);
                throw new GenericException(msg, msg);
            }

            return html.CheckPermissions(partes[0], partes[1]);
        }


        #endregion
    }
}