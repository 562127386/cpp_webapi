using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace com.cpp.calypso.webapi
{
    /// <summary>
    /// Manejo global de la aplicación
    /// </summary>
    public sealed class GenericApiApplication : IBaseApplication
    {
        private IManagerDateTime _managerDateTime;
        private ILogger log;

 
        public GenericApiApplication(
            IManagerDateTime managerDateTime,
            ILoggerFactory iLoggerFactory)
        {
            
            _managerDateTime = managerDateTime;

            log = iLoggerFactory.Create(typeof(GenericApiApplication));

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
                ClaimsPrincipal principal = HttpContext.Current.User as ClaimsPrincipal;
                if (principal == null)
                    return false;


                // Get the claims values
                var claimUser = principal.Claims.Where(c => c.Type == Constantes.API_CLAIM_USUARIO_AUTENTIFICADO)
                                   .Select(c => c.Value).SingleOrDefault();


                if (claimUser == null)
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
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


            ClaimsPrincipal principal = HttpContext.Current.User as ClaimsPrincipal;
            if (principal == null)
                throw new GenericException("No se encuentra autentificado. Principal deberia ser de tiempo ClaimsPrincipal y diferente de nulo", "No se encuentra autentificado");


            // Get the claims values
            var claimUser = principal.Claims.Where(c => c.Type == Constantes.API_CLAIM_USUARIO_AUTENTIFICADO)
                               .Select(c => c.Value).SingleOrDefault();

            if (claimUser == null) {
                var error = string.Format("No se encuentra autentificado. No existe Claim {0}",Constantes.API_CLAIM_USUARIO_AUTENTIFICADO);
                throw new GenericException(error, "No se encuentra autentificado");
            }

            return JsonConvert.DeserializeObject<UsuarioAutentificado>(claimUser);

         }

        /// <summary>
        /// Establecer el usuario actual
        /// </summary>
        /// <param name="usuario"></param>
        public void SetCurrentUser(UsuarioAutentificado usuario)
        {
            string error = string.Format("Metodo en WebApi no soportado", GetCurrentUser().Cuenta);
            throw new GenericException(error, error);
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
            string error = string.Format("Metodo en WebApi no soportado", GetCurrentUser().Cuenta);
            throw new GenericException(error, error);

        }

        public ModuloAutentificado GetCurrentModule()
        {
            string error = string.Format("Metodo en WebApi no soportado", GetCurrentUser().Cuenta);
            throw new GenericException(error, error);
        }
    }
}