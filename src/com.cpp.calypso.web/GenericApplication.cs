using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using System;
using System.Web;

namespace com.cpp.calypso.web
{
    /// <summary>
    /// Manejo global de la aplicación
    /// </summary>
    public sealed class GenericApplication : IBaseApplication
    {
        private IManagerDateTime _managerDateTime;
        private ILogger log;

 
        public GenericApplication(
            IManagerDateTime managerDateTime,
            ILoggerFactory iLoggerFactory)
        {
            
            _managerDateTime = managerDateTime;

            log = iLoggerFactory.Create(typeof(GenericApplication));

            log.DebugFormat("Call GenericApplication.Construct");
        }


        /// <summary>
        /// Verificar si la aplicacion esta autentificada
        /// </summary>
        /// <returns></returns>
        public bool IsAuthenticated()
        {

            if (HttpContext.Current == null)
                return false;

            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return false;


            try
            {
                var user =
                    HttpContext.Current.Session[ConstantesSesionesSecurity.SESSION_USUARIO_AUTENTIFICADO] as UsuarioAutentificado;
                if (user == null)
                    return false;

                var modulo = HttpContext.Current.Session[ConstantesSesiones.SESSION_MODULO] as ModuloAutentificado;
                if (modulo == null)
                    return false;

            }
            catch (Exception)
            {
                //TODO: Dato quemado. Mejorar
                //JSA. El direccionamiento debe ser configurable, no debe existir datos quemadas.
                //en este caso se direcciona al controlar cuenta, y accion salir, pero eso debe ser
                //configurada por la aplicacion concreta. 
                HttpContext.Current.Response.RedirectToRoute(new { controller = "Acceso", action = "Salir" });
                HttpContext.Current.Response.End();
            }


            return true;
        }


        /// <summary>
        /// Obtiene la informacion del usuario autenticado
        /// </summary>        
        public UsuarioAutentificado GetCurrentUser()
        {
            //TODO: JSA.  Pasar nullo el usuario, o lanzar excepcion 
            if (HttpContext.Current == null)
                throw new GenericException("No se encuentra autentificado, el objeto HttpContext.Current es nulo", "No se encuentra autentificado");


            //TODO: JSA, Revisar cual seria el flujo 
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                throw new GenericException("No se encuentra autentificado", "No se encuentra autentificado");


            //TODO. JSA, Usuario, traer con todos los roles ?? 
            if (HttpContext.Current.Session[ConstantesSesionesSecurity.SESSION_USUARIO_AUTENTIFICADO] != null)
            {
                var usuarioSesion = HttpContext.Current.Session[ConstantesSesionesSecurity.SESSION_USUARIO_AUTENTIFICADO] as UsuarioAutentificado;

                return usuarioSesion;
            }

            //TODO: FORZAR CERRAR SESSION.
            //HttpContext.Current.Session.Clear();
            //return null;

            throw new GenericException(string.Format("No se encuentra autentificado, la Session {0} es nula ", ConstantesSesionesSecurity.SESSION_USUARIO_AUTENTIFICADO), "No se encuentra autentificado");
        }

        /// <summary>
        /// Establecer el usuario actual
        /// </summary>
        /// <param name="usuario"></param>
        public void SetCurrentUser(UsuarioAutentificado usuario)
        {
            Guard.AgainstArgumentNull(usuario, "GenericApplication.usuario");

            HttpContext.Current.Session[ConstantesSesionesSecurity.SESSION_USUARIO_AUTENTIFICADO] = usuario;
        }

          
         
     
        /// <summary>
        /// Obtener el fecha y tiempo 
        /// </summary>
        /// <returns></returns>
        public DateTime getDateTime()
        {
            return _managerDateTime.Get();
        }

       

        public void SetCurrentModule(ModuloAutentificado modulo) 
        {
            Guard.AgainstArgumentNull(modulo, "GenericApplication.modulo");

            HttpContext.Current.Session[ConstantesSesiones.SESSION_MODULO] = modulo;

        }

        public ModuloAutentificado GetCurrentModule()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                throw new GenericException("No se encuentra autentificado", "No se encuentra autentificado");

            if (HttpContext.Current != null && HttpContext.Current.Session[ConstantesSesiones.SESSION_MODULO] != null)
            {
                var modulo = HttpContext.Current.Session[ConstantesSesiones.SESSION_MODULO] as ModuloAutentificado;
                return modulo;
            }

            //TODO: FORZAR CERRAR SESSION.
            //HttpContext.Current.Session.Clear();
            //return null;

            string error = string.Format("No se encuentra en la session el modulo autentificado para el usuario [{0}]", GetCurrentUser().Cuenta);
            throw new GenericException(error, error);
        }
    }
}