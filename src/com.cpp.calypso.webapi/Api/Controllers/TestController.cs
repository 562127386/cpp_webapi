using System;
using System.Web.Http;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;

namespace com.cpp.calypso.webapi.Api.Controllers
{
    public class TestController : BaseApiController
    {
        public TestController(
            IApplication application,
            IHandlerExcepciones manejadorExcepciones) : base(manejadorExcepciones)
        {
            Application = application;
        }

        public IApplication Application { get; }

        public UsuarioAutentificado GetUsuario() {
            return Application.GetCurrentUser();
        }

        [AllowAnonymous]
        [HttpGet]
        //[ActionName("Error")]
        [Route("~/api/test/Error")]
        public string Error()
        {
            throw new Exception("Exception para pruebas web api");
        }
    }
}
