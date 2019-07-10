using com.cpp.calypso.framework;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;
using com.cpp.calypso.webapi.Api.Controllers.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;

namespace com.cpp.calypso.webapi.Api.Controllers
{
    [System.Web.Http.AllowAnonymous]
    public class SincronizacionManualController : BaseApiController
    {
        private readonly IRespaldoMovilAsyncBaseCrudAppService _respaldoMovilService;

        public SincronizacionManualController(
            IHandlerExcepciones manejadorExcepciones,
            IRespaldoMovilAsyncBaseCrudAppService respaldoMovilService
            ) : base(manejadorExcepciones)
        {
            _respaldoMovilService = respaldoMovilService;
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult SincronizacionManual(SincronizacionManualModel model)
        {
            var respaldoUsuariosBase64Bytes = Convert.FromBase64String(model.JsonRespaldo);
            var json = System.Text.Encoding.UTF8.GetString(respaldoUsuariosBase64Bytes);

            _respaldoMovilService.Create(json, model.UserId);

            return new JsonResult
            {
                Data = new { success = true, result = true}
            };
        }
    }
}