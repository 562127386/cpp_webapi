using System.Web.Mvc;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Clase base para los controladores de MVC 
    /// </summary>
    /// 
    //[UserInfo]
    public abstract class BaseController : Controller
    {
        private readonly IHandlerExcepciones _manejadorExcepciones;

        protected BaseController(IHandlerExcepciones manejadorExcepciones)
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
