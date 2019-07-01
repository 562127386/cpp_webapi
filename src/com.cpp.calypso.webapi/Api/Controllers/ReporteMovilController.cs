using System.Collections.Generic;
using System.Web.Http;
using com.cpp.calypso.framework;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;
using com.cpp.calypso.proyecto.dominio.Entidades;

namespace com.cpp.calypso.webapi.Api.Controllers
{
    [AllowAnonymous]
    public class ReporteMovilController : BaseApiController
    {
        private readonly IReporteMovilAsyncBaseCrudAppService _reporteMovilService;

        public ReporteMovilController(
            IHandlerExcepciones manejadorExcepciones,
            IReporteMovilAsyncBaseCrudAppService reporteMovilService
            ) : base(manejadorExcepciones)
        {
            _reporteMovilService = reporteMovilService;
        }

        public List<ReporteMovil> GetReporteMovil(int servicioId, string rol)
        {
            var reportes = _reporteMovilService.GetReportesMovilPorRolServicio(rol, servicioId);
            return reportes;
        }
    }
}