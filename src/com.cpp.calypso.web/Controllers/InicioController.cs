using Abp.Domain.Repositories;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using System;
using System.Web.Mvc;

namespace com.cpp.calypso.web
{
    public class InicioController : BaseController
    {
        private readonly IApplication _application;
        private readonly IParametroService _parametroService;
       
        public InicioController(IHandlerExcepciones manejadorExcepciones, IApplication application, IParametroService parametroService)
            : base(manejadorExcepciones)
        {
            _application = application;
            _parametroService = parametroService;
            
        }

        // GET: Inicio
        public ActionResult Index(string Texto, TipoMensaje? Tipo)
        {

            var usuario = _application.GetCurrentUser();

            ViewBag.NOMBRE_USUARIO = string.Format("{0} {1}", usuario.Nombres,usuario.Apellidos);

            ViewBag.MODULO = _application.GetCurrentModule().Nombre;

            ViewBag.Texto = Texto;
            ViewBag.Tipo = Tipo;
            
            return View();
        }

        [AllowAnonymous, ChildActionOnly]
        public PartialViewResult Hora()
        {
            TimeSpan hora = _application.getDateTime().TimeOfDay;
            return PartialView("_Hora", hora);
        }
    }
}