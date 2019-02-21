using System;
using System.Web.Mvc;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;

namespace com.cpp.calypso.web.Controllers
{
    public class SistemaController : BaseController
    {
        IApplication _application;
        ICacheManager _cacheManager;


        public SistemaController(IHandlerExcepciones manejadorExcepciones, IApplication application, ICacheManager cacheManager)
            : base(manejadorExcepciones)
        {

            _application = application;
            _cacheManager = cacheManager;
        }


        // GET: Sistema
        public ActionResult Index()
        {
            ViewBag.NombreUsuario = _application.GetCurrentUser().Nombres + " " +
            _application.GetCurrentUser().Apellidos;
            return View();
        }

        public ActionResult LimpiarCache()
        {
            try
            {
                _cacheManager.Flush();
            }
            catch (Exception ex)
            {
                   return this.ExceptionResult(ManejadorExcepciones.HandleException(ex));
            }
           
            return Json(true);

        }
    }
}