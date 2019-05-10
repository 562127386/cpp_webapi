using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using CommonServiceLocator;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;
using com.cpp.calypso.webapi.Api.Controllers.Models;
using System.Web.Mvc;
using System.Web;

namespace com.cpp.calypso.webapi.Api.Controllers
{
    [System.Web.Http.AllowAnonymous]
    public class AuthController : BaseApiController
    {
        private readonly IUsuarioMovilAsyncBaseCrudAppService _usuarioService;

        public AuthController(
            IHandlerExcepciones manejadorExcepciones,
            IUsuarioMovilAsyncBaseCrudAppService usuarioService
            ) : base(manejadorExcepciones)
        {
            _usuarioService = usuarioService;
        }


        public ActionResult Authenticate(AuthPinModel login)
        {
            var user = _usuarioService.FindByUsernamePin(login.UsernameOrEmailAddress, login.Pin);

            if (user != null)
            {
                return new JsonResult
                {
                    Data = new { success = true, result = user}
                };
            }

            var result = string.Format("Usuario ó Clave incorrecta");
            return new JsonResult
            {
                Data = new { success = false, result }
            };


        }

    }
}
