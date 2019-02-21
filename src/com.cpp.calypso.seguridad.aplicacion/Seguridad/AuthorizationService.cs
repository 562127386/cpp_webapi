using System.Linq;
using com.cpp.calypso.framework;
using CommonServiceLocator;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.comun.aplicacion;
using System.Threading.Tasks;

namespace com.cpp.calypso.seguridad.aplicacion
{
    /// <summary>
    /// Servicio de autorizacion
    /// </summary>
    public class AuthorizationService : IAuthorizationService
    {

        static readonly ILogger log =
         ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(AuthorizationService));

 
        IApplication _application;
        ICacheManager _cacheManager;
        IFuncionalidadService _funcionalidadService;
        private readonly IModuloService ModuloService;
        private readonly IRolService RolService;

        public AuthorizationService(IApplication application, 
            ICacheManager cacheManager,
            IFuncionalidadService funcionalidadService,
            IModuloService moduloService,
            IRolService rolService)
        {
            _application = application;
            _cacheManager = cacheManager;
            _funcionalidadService = funcionalidadService;
            this.ModuloService = moduloService;
            RolService = rolService;
        }


        public async Task<bool> Authorize(string funcionalidadCodigo, string actionCodigo)
        {
            Guard.AgainstNullOrEmptyString(funcionalidadCodigo, "funcionalidadCodigo");
            Guard.AgainstNullOrEmptyString(actionCodigo, "actionCodigo");

            //1. Recuperar funcionalides
            var funcionalidades = _funcionalidadService.GetFuncionalidades();

            //2. Recuperar funcionalidad se verificar su autorizacion
            var funcionalidad = funcionalidades.Where(fun => fun.Codigo == funcionalidadCodigo).SingleOrDefault();
            if (funcionalidad == null)
            {
                //TODO: JSA, lanzar excepcion si no existe funcionalidad o retornar que no se encuentra autorizado ?
                log.WarnFormat("No existe la funcionalidad con el codigo [{0}]", funcionalidadCodigo);
                return false;
            }

            var accion = funcionalidad.Acciones.Where(acc => acc.Codigo == actionCodigo).SingleOrDefault();
            if (accion == null)
            {
                //TODO: JSA, lanzar excepcion si no existe accion / funcionalidad o retornar que no se encuentra autorizado ?
                log.WarnFormat("No existe la accion [{0}] en la  funcionalidad con el codigo [{1}]", actionCodigo, funcionalidadCodigo);
                return false;
            }

            return await Authorize(accion);
        }

        public async Task<bool> Authorize(Accion action)
        {
            Guard.AgainstArgumentNull(action, "action");

          

            //1. Obtener usuario actual
            var usuario = _application.GetCurrentUser();

            if (usuario == null)
                return false;

            log.DebugFormat("Verificar permiso de la accion {0} en la funcionalidad {1} para el usuario [{2}-{3}]", action.Codigo, action.FuncionalidadId,
                    usuario.Cuenta, usuario.Nombres);


            //Verificar si la accion se encuentra en las funcionalidades del modulo autentificado
            var modulo = _application.GetCurrentModule();
            var verificarAccionModuloAutentificado = 
                (await ModuloService.GetModuleAndFunctionality(modulo.Id)).Funcionalidades.Any(f => f.Id == action.FuncionalidadId);

            if (!verificarAccionModuloAutentificado) {

                log.DebugFormat("La accion {0}, no se encuentra en las funcionalidades del modulo autentificado {1}", action.Codigo,modulo.Codigo);
                return false;
            }
            

            if (usuario.Roles.Where(r => r.EsAdministrador).Any())
            {
                log.DebugFormat("El usuario {0}, posee un rol que es administrador", usuario.Cuenta);
                return true;
            }

            //1. Obtener listado de Roles/Permisos asociadas al usuario autentificado
            var roles = (await RolService.GetAllRolAndPermissions())
                        .Where(r => usuario.Roles.Any(rUser => rUser.Id  == r.Id)).ToList();

            
            //2. Recuperar Permisos que posee el rol Autentificado
            var permisos = (from r in roles
                            where r.Permisos.Any(p => p.AccionId == action.Id)
                            select r.Permisos).ToList();

         
            if (permisos.Any())
                return true;

            return false;
        }
    }
}
