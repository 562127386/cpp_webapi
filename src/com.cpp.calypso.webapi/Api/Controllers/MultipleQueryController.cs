using com.cpp.calypso.framework;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;
using System;
using System.IO;
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

        public JsonResult DoQuery()
        {
            var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var bodyText = bodyStream.ReadToEnd();


            string error = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _catalogoService.RealizarMultiplesConsultas(bodyText);

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