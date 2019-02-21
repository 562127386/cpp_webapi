using Abp.Threading;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using com.cpp.calypso.seguridad.aplicacion;
using CommonServiceLocator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;


namespace  com.cpp.calypso.web
{


    /// <summary>
    /// Metodos de ayuda para mapear el mecanismo de seguridad del SGA a controles de MVC
    /// </summary>
    public class SecurityControllerHelper
    {

        static readonly ILogger log =
           ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(SecurityControllerHelper));

  

        /// <summary>
        /// Obtener funcionalidades desde la informacion del controlador. Las funcionalidades tiene asociado el nombre del controlador.
        /// </summary>
        /// <param name="funcionalidades"></param>
        /// <param name="controllerDescriptor"></param>
        /// <returns></returns>
        public static Funcionalidad ControllerToFunctionality(IEnumerable<Funcionalidad> funcionalidades, 
            ControllerDescriptor controllerDescriptor)
        {
            Guard.AgainstArgumentNull(funcionalidades, "funcionalidades");

            //TODO: JSA. SE PUEDE MEJORAR SE CREA UN MAPPER (NOMBRE CONTROLLAR, FUNCIONALIDAD)

            //Buscar funcionalidades que tenga el nombre del controlador
            var funcionalidadesRelControlador = from f in funcionalidades
                                                where (f.Controlador.ToUpper().Equals(controllerDescriptor.ControllerName.ToUpper()))
                                                select f;

            var list = funcionalidadesRelControlador.ToList();

            log.DebugFormat("Resultado de convertir nombre de controlador : [{0}] a funcionalidad del sistema : [{1}], cantidad de resultados que coincide : [{2}]", controllerDescriptor.ControllerName.ToUpper(), (list.Count == 1 ? list[0].Nombre : ""), list.Count.ToString());

            if (list.Count == 0)
                return null;

            if (list.Count > 1) {
                //No puede existir varias funcionalidades mapeadas al mismo controlador ??? 
                var fun = string.Join(", ", funcionalidades.Select(e => e.Nombre));
                throw new ArgumentException(string.Format("Existe varias funcionalidades [{0}] que tiene el mismo nombre de controlador [{1}], un total de [{2}] funcionalidades. Un controlador solo puede estar asociada a una funcionalidad", fun, controllerDescriptor.ControllerName, list.Count));
            }

            return list[0];
        }

        public static Funcionalidad ControllerToFunctionality(IEnumerable<Funcionalidad> funcionalidades,
           string controllerName)
        {
            Guard.AgainstArgumentNull(funcionalidades, "funcionalidades");

            //TODO: JSA. SE PUEDE MEJORAR SE CREA UN MAPPER (NOMBRE CONTROLLAR, FUNCIONALIDAD)

            //Buscar funcionalidades que tenga el nombre del controlador
            var funcionalidadesRelControlador = from f in funcionalidades
                                                where (f.Controlador.ToUpper().Equals(controllerName.ToUpper()))
                                                select f;

            var list = funcionalidadesRelControlador.ToList();

            log.DebugFormat("Resultado de convertir nombre de controlador : [{0}] a funcionalidad del sistema : [{1}], cantidad de resultados que coincide : [{2}]", controllerName.ToUpper(), (list.Count == 1 ? list[0].Nombre : ""), list.Count.ToString());

            if (list.Count == 0)
                return null;

            if (list.Count > 1)
            {
                //No puede existir varias funcionalidades mapeadas al mismo controlador ??? 
                var fun = string.Join(", ", funcionalidades.Select(e => e.Nombre));
                log.WarnFormat("Existe varias funcionalidades [{0}] que tiene el mismo nombre de controlador [{1}], un total de [{2}] funcionalidades. Un controlador solo puede estar asociada a una funcionalidad", fun, controllerName, list.Count);
                return null;
            }

            return list[0];
        }

        /// <summary>
        /// Mapear las acciones de un controlador MVC con las acciones de Seguridad
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public static Accion ActionControllerToActionFunctionality(IRepositoryAuthorizationFilter authorizationFilter,Funcionalidad funcionalidad, ActionDescriptor actionDescriptor)
        {
            //Utilizar Sinonimos 
            string accion = string.Empty;

            var listAction = from i in authorizationFilter.GetListSynonymousAction()
                             where actionDescriptor.ActionName.StartsWith(i.Key, StringComparison.OrdinalIgnoreCase)
                       select i;

            if (listAction.Count() == 1)
                accion = listAction.ToList()[0].Value;

            if (listAction.Count() > 1)
                throw new Exception(string.Format("Existe varias coincidencias entre el nombre de la accion [{0}], al mapper sinominos de acciones, a la accion estandar: [{1}]", actionDescriptor.ActionName, listAction.ToList()[0].Value));

            //Buscar la accion si existe en la funcionalidad, o el sinomimo
            var listAccionesFuncionalidad = from i in funcionalidad.Acciones
                                            where actionDescriptor.ActionName.StartsWith(i.Codigo, StringComparison.OrdinalIgnoreCase) ||
                                            accion.StartsWith(i.Codigo, StringComparison.OrdinalIgnoreCase)
                                            select i;

            if (listAccionesFuncionalidad.Count() == 1)
                return listAccionesFuncionalidad.ToList()[0];

            if (listAccionesFuncionalidad.Count() > 1)
                throw new Exception(string.Format("Existe varias coincidencias entre el nombre de la accion MVC [{0}], al mapper con las acciones de la funcionalidad [{1}-{2}]", actionDescriptor.ActionName, funcionalidad.Codigo, funcionalidad.Nombre));


            // if (list.Count() == 0)
            throw new Exception(string.Format("No existe coincidencias entre el nombre de la accion MVC [{0}], al mapper con las acciones de la funcionalidad [{1}-{2}] ", actionDescriptor.ActionName, funcionalidad.Codigo, funcionalidad.Nombre));
        }


        public static Accion ActionControllerToActionFunctionality(IRepositoryAuthorizationFilter authorizationFilter, Funcionalidad funcionalidad, string actionName)
        {
            //Utilizar Sinonimos 
            string accion = string.Empty;

            var listAction = from i in authorizationFilter.GetListSynonymousAction()
                             where actionName.StartsWith(i.Key, StringComparison.OrdinalIgnoreCase)
                             select i;

            if (listAction.Count() == 1)
                accion = listAction.ToList()[0].Value;

            if (listAction.Count() > 1)
                throw new Exception(string.Format("Existe varias coincidencias entre el nombre de la accion [{0}], al mapper sinominos de acciones, a la accion estandar: [{1}]", actionName, listAction.ToList()[0].Value));

            //Buscar la accion si existe en la funcionalidad, o el sinomimo
            var listAccionesFuncionalidad = from i in funcionalidad.Acciones
                                            where actionName.StartsWith(i.Codigo, StringComparison.OrdinalIgnoreCase) ||
                                            accion.StartsWith(i.Codigo, StringComparison.OrdinalIgnoreCase)
                                            select i;

            if (listAccionesFuncionalidad.Count() == 1)
                return listAccionesFuncionalidad.ToList()[0];

            return null;

        }

        /// <summary>
        /// Si la acion no es considerada en la verificacion de autorizacion. Ejemplo Validar<Regla>, acciones para validaciones remotadas
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public static bool SkipActionSecurity(IRepositoryAuthorizationFilter authorizationFilter,ActionDescriptor actionDescriptor)
        {

            var filter = from i in authorizationFilter.GetListSkipAction()
                         where actionDescriptor.ActionName.StartsWith(i, StringComparison.OrdinalIgnoreCase)
                         select i;

            var list = filter.ToList();

            log.DebugFormat("Resultado de omitir action : [{0}]. Cantidad de elementos que coincide con la accion desde el listado de omitidos : [{1}]", actionDescriptor.ActionName, list.Count);

            return (list.Count > 0);

        }

        /// <summary>
        /// Si el controlador no se debe aplicar autorizacion. Ejemplo pagina de inicio del sitio web.
        /// </summary>
        /// <param name="controllerDescriptor"></param>
        /// <returns></returns>
        public static bool SkipControllerSecurity(IRepositoryAuthorizationFilter authorizationFilter,ControllerDescriptor controllerDescriptor)
        {

            var filter = from i in authorizationFilter.GetListSkipController()
                         where controllerDescriptor.ControllerName.Equals(i, StringComparison.OrdinalIgnoreCase)
                         select i;

            var list = filter.ToList();

            log.DebugFormat("Resultado de omitir controladores : [{0}]. Cantidad de elementos que coincide con la nombre  de controlador desde el listado de omitidos : [{1}]", controllerDescriptor.ControllerName, list.Count);

            return (list.Count > 0);
        }

        /// <summary>
        /// Verificar si controlador / accion no se debe aplicar auntentificacion.
        /// 1. Saltar autorizacion, si el controlador o la accion tiene el atributo AllowAnonymousAttribute
        /// 2.1 Controladores o acciones que no se debe verificar
        /// 2.2 Atributos explicitos en los controladores que indica que no se debe verificar la autorizacion
        /// </summary>
        /// <param name="authorizationFilter"></param>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public static bool SkipControllerActionSecurity(IRepositoryAuthorizationFilter authorizationFilter, ActionDescriptor actionDescriptor)
        {
            
            bool isAllowAnonymousAttribute =
                actionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
             || actionDescriptor.ControllerDescriptor.IsDefined(
                typeof(AllowAnonymousAttribute), inherit: true);


            if (isAllowAnonymousAttribute)
                return true;


            if (SkipControllerSecurity(authorizationFilter, actionDescriptor.ControllerDescriptor))
                return true;


            return SkipActionSecurity(authorizationFilter, actionDescriptor);
        }

      


        public static bool CheckPermissions(string controllerName,string actionName) {

            //TODO:
            //AllowAnonymous atributos en los controller y accciones


            var servicioFuncionalidad = ServiceLocator.Current.GetInstance<IFuncionalidadService>();

            var funcionalidadRelacionada = SecurityControllerHelper.ControllerToFunctionality(servicioFuncionalidad.GetFuncionalidades(), controllerName);

            if (funcionalidadRelacionada == null)
            {
                return false;
            }

            var accion = SecurityControllerHelper.ActionControllerToActionFunctionality(RepositoryAuthorizationFilter.Instance(), funcionalidadRelacionada, actionName);

            if (accion == null)
            {
                return false;
            }

            var servicio = ServiceLocator.Current.GetInstance<IAuthorizationService>();

            var autorizado = AsyncHelper.RunSync(() => servicio.Authorize(accion));

            return autorizado;
        }


    }

    /// <summary>
    /// Repository get data for apply authorization (action, controller, Synonymous action) 
    /// </summary>
    public interface IRepositoryAuthorizationFilter
    {

        /// <summary>
        /// Get List action skip authorize
        /// </summary>
        /// <returns></returns>
        List<string> GetListSkipAction();

        /// <summary>
        /// Get List Controller skip autorize
        /// </summary>
        /// <returns></returns>
        List<string> GetListSkipController();


        /// <summary>
        /// Get List Name Synonymous Action
        /// </summary>
        /// <returns></returns>
        HashSet<KeyValuePair<string, string>> GetListSynonymousAction();
    }


    public class RepositoryAuthorizationFilter : IRepositoryAuthorizationFilter
    {
        private static RepositoryAuthorizationFilter _instance;

        public static RepositoryAuthorizationFilter Instance()
        {
            if (_instance == null)
                _instance = new RepositoryAuthorizationFilter();
            return _instance;
        }


        /// <summary>
        /// Listado de prefijos de acciones que no se debe aplicar autorizacion
        /// </summary>
        private List<string> _ListSkipAction;
        private List<string> ListSkipAction
        {
            get
            {
                if (_ListSkipAction == null)
                {
                    //Acciones que se debe omitir no se debe aplicar autorizacion
                    var mapperOmitir = ConfigurationManager.AppSettings[ConstantesConfiguraciones.CLAVE_CONFIGURACION_SEGURIDAD_AUTORIZACION_ACCIONES_OMITIR];
                    _ListSkipAction = new List<string>();

                    if (!string.IsNullOrWhiteSpace(mapperOmitir))
                    {
                        //Existe configuracion
                        foreach (var i in mapperOmitir.Split(','))
                        {
                            _ListSkipAction.Add(i.ToUpper());
                        }
                    }
                }
                return _ListSkipAction;
            }
        }


        /// <summary>
        /// Listado de controladores que se deben omitir
        /// </summary>
        private List<string> _ListSkipController;
        private List<string> ListSkipController
        {
            get
            {
                if (_ListSkipController == null)
                {
                    var controllerSkip = ConfigurationManager.AppSettings[ConstantesConfiguraciones.CLAVE_CONFIGURACION_SEGURIDAD_AUTORIZACION_CONTROLADORES_OMITIR];

                    _ListSkipController = new List<string>();

                    if (!string.IsNullOrWhiteSpace(controllerSkip)) {
                        //Existe configuracion
                        foreach (var i in controllerSkip.Split(','))
                        {
                            _ListSkipController.Add(i.ToUpper());
                        }
                    }
                }
                return _ListSkipController;
            }
        }

        private static HashSet<KeyValuePair<string, string>> _MapperSynonymous;
        private static HashSet<KeyValuePair<string, string>> MapperSynonymous
        {
            get
            {
                if (_MapperSynonymous == null)
                {

                    var mapperSinonimos = ConfigurationManager.AppSettings[ConstantesConfiguraciones.CLAVE_CONFIGURACION_SEGURIDAD_AUTORIZACION_ACCIONES_CRUD_SINOMINOS];

                     _MapperSynonymous = new HashSet<KeyValuePair<string, string>>();


                    if (!string.IsNullOrWhiteSpace(mapperSinonimos))
                    {
                        //SI existe configuracion

                        Dictionary<string, string> listaSinomios = JsonConvert.DeserializeObject<Dictionary<string, string>>(mapperSinonimos);

                        foreach (var item in listaSinomios)
                        {

                            foreach (var sinonimo in item.Value.Split(','))
                            {
                                _MapperSynonymous.Add(new KeyValuePair<string, string>(sinonimo, item.Key));
                            }

                        }
                    }
                    
                }
                return _MapperSynonymous;
            }
        }

        public HashSet<KeyValuePair<string, string>> GetListSynonymousAction()
        {
            return MapperSynonymous;
        }


        public List<string> GetListSkipAction()
        {
            return ListSkipAction;
        }

        public List<string> GetListSkipController()
        {
            return ListSkipController;
        }
    }

}