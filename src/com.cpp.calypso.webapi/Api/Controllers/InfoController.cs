using System.Web.Http;
using com.cpp.calypso.framework;

namespace com.cpp.calypso.webapi.Controllers
{
    [AllowAnonymous]
    public class InfoController : BaseApiController
    {
        public InfoController(IHandlerExcepciones manejadorExcepciones) :
            base(manejadorExcepciones)
        {
        }

        public string Get()
        {
            return "ok";
        }
    }
}
