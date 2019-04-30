using com.cpp.calypso.framework;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.cpp.calypso.webapi.Api.Controllers
{
    [AllowAnonymous]
    public class QueryController : BaseApiController
    {
        private readonly ICatalogoAsyncBaseCrudAppService _catalogoService;

        public QueryController(
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
                    var result = _catalogoService.DoQuery(query);

                    return new JsonResult
                    {
                        Data = new { success = true, result }
                    };

                }
            }
            catch (Exception ex)
            {
                var result = ManejadorExcepciones.HandleException(ex);
                error = result.Message;
                return new JsonResult
                {
                    Data = new { success = false, errors = error}
                };
            }
         
            return new JsonResult
            {
                Data = new { success = false, errors = error }
            };

        }

    }
}
