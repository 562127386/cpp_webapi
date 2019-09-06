using System.Web.Mvc;
using com.cpp.calypso.framework;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;

namespace com.cpp.calypso.webapi.Api.Controllers
{
    [System.Web.Http.AllowAnonymous]
    public class SolicitudPinController : BaseApiController
    {
        private readonly ISolicitudPinAsyncBaseCrudAppService _solicitudPinService;

        // GET: SolicitudPin
        public SolicitudPinController(
            IHandlerExcepciones manejadorExcepciones,
            ISolicitudPinAsyncBaseCrudAppService solicitudPinService
            ) : base(manejadorExcepciones)
        {
            _solicitudPinService = solicitudPinService;
        }


        
        [System.Web.Mvc.HttpPost]
        public ActionResult SolicitarPin(string correo)
        
        
        {
             _solicitudPinService.SolicitarPin(correo);

            return new JsonResult
            {
                Data = new { success = true, result = "Su solicitud fué registrada exitosamente, en unos momentos recibirá un correo electrónico." }
            };
        }
    }
}