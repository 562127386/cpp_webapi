
using Abp.WebApi.Controllers;
using com.cpp.calypso.framework;
using System.Web.Http;

namespace  com.cpp.calypso.webapi
{
    /// <summary>
    /// Clase base para los controladores de Web API
    /// </summary>
    /// 
    //[UserInfo]
    public abstract class BaseApiController : AbpApiController
    {
        private readonly IHandlerExcepciones _manejadorExcepciones;

        protected BaseApiController(IHandlerExcepciones manejadorExcepciones)
        {
            _manejadorExcepciones = manejadorExcepciones;
        }

        /// <summary>
        /// Objeto para manejar excepciones
        /// </summary>
        protected IHandlerExcepciones ManejadorExcepciones
        {
            get { return _manejadorExcepciones; }
        }

           
    }

    
    
}
