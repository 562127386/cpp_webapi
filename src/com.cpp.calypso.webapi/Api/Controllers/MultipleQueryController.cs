using com.cpp.calypso.framework;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.cpp.calypso.webapi.Api.Controllers
{
    [System.Web.Http.AllowAnonymous]
    public class MultipleQueryController : BaseApiController
    {
        private readonly ICatalogoAsyncBaseCrudAppService _catalogoService;

        public MultipleQueryController(
           IHandlerExcepciones manejadorExcepciones,
           ICatalogoAsyncBaseCrudAppService catalogoService
           ) : base(manejadorExcepciones)
        {
            _catalogoService = catalogoService;
        }

        public JsonResult DoQuery(string query)
        {
            string error = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _catalogoService.RealizarMultiplesConsultas(query);

                    return new JsonResult
                    {
                        Data = data
                    };

                }
            }
            catch (Exception ex)
            {
                var result = ManejadorExcepciones.HandleException(ex);
                error = result.Message;
                return new JsonResult
                {
                    Data = new { success = false, errors = error }
                };
            }

            return new JsonResult
            {
                Data = new { success = false, errors = error }
            };

        }
    }
}