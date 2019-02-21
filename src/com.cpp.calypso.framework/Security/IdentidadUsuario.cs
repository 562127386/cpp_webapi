using com.cpp.calypso.comun.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Clase para gestion de identidades de usuarios
    /// </summary>
    public class IdentityUser : IIdentityUser
    {

        /// <summary>
        /// Obtener el identificador de la identidad del usuario
        /// </summary>
        /// <returns></returns>
        public int GetCurrentIdentity()
        {
            IUsuario usuario = null;

            //TODO: JSA, definir si se guarda el identificador del usuario o su nombre
            //Opcion 1. Guardar el identificador en el nombre del usuario
            //Opcion 2. Recuperar el usuario actual desde la aplicacion

            //string identityName = Thread.CurrentPrincipal.Identity.Name;
            int identityUsuario = -1; //TODO: JSA, guardar en configruacion
            //int.TryParse(identityName, out identityUsuario);



            //TODO. JSA, Usuario, traer con todos los roles ?? 
            if (HttpContext.Current != null && 
                HttpContext.Current.User.Identity.IsAuthenticated && 
                HttpContext.Current.Session[ConstantesSesionesSecurity.SESSION_USUARIO_AUTENTIFICADO] != null)
            {
                usuario = HttpContext.Current.Session[ConstantesSesionesSecurity.SESSION_USUARIO_AUTENTIFICADO] as IUsuario;
 
            }


            if (usuario != null)
                return usuario.Id;

            return identityUsuario;
 
        }
    }
}
