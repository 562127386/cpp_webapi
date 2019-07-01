using com.cpp.calypso.framework;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;

namespace com.cpp.calypso.webapi.Api.Controllers
{
    [System.Web.Http.AllowAnonymous]
    public class SolicitudReporteMovilController : BaseApiController
    {
        private readonly ISolicitudReporteMovilAsyncBaseCrudAppService _solicitudReporteService;
        private readonly IReporteMovilAsyncBaseCrudAppService _reporteMovilService;

        public SolicitudReporteMovilController(
            IHandlerExcepciones manejadorExcepciones,
            ISolicitudReporteMovilAsyncBaseCrudAppService solicitudReporteService
            ) : base(manejadorExcepciones)
        {
            _solicitudReporteService = solicitudReporteService;
        }


        public async Task<ActionResult> Create(SolicitudReporteMovilDto solicitud)
        {
            if (ModelState.IsValid)
            {
               var dto =  await _solicitudReporteService.Create(solicitud);
                return new JsonResult
                {
                    Data = new { success = true, result = dto }
                };
            }
            return new JsonResult
            {
                Data = new { success = false, result = "Solicitud incorrecta" }
            };

        }
    }
}