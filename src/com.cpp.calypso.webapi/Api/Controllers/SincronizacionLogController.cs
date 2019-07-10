using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using com.cpp.calypso.framework;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;
using com.cpp.calypso.webapi.Api.Controllers.Models;

namespace com.cpp.calypso.webapi.Api.Controllers
{
    [System.Web.Http.AllowAnonymous]
    public class SincronizacionLogController : BaseApiController
    {
        private readonly ISincronizacionLogAsyncBaseCrudAppService _sincronizacionLogService;

        public SincronizacionLogController(
            IHandlerExcepciones manejadorExcepciones,
            ISincronizacionLogAsyncBaseCrudAppService sincronizacionLogService
            ) : base(manejadorExcepciones)
        {
            _sincronizacionLogService = sincronizacionLogService;
        }


        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> GuardarLog(SincronizacionLogModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new SincronizacionLogDto()
                {
                    FechaRegistro = DateTime.Now,
                    Log = model.Log,
                    UsuarioId = model.UsuarioId
                };

                await _sincronizacionLogService.Create(entity);
                return new JsonResult
                {
                    Data = new { success = true }
                };
            }
            return new JsonResult
            {
                Data = new { success = false }
            };
        }
        
    }
}