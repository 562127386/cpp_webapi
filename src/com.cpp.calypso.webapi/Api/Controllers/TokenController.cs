using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using CommonServiceLocator;

namespace com.cpp.calypso.webapi.Controllers
{
    [AllowAnonymous]
    public class TokenController : BaseApiController
    {
        //Logger de la aplicacion
        private static readonly ILogger Log =
            ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(TokenController));

       
        public LoginManager<Usuario, Modulo, Rol> LoginManager { get; }

        public TokenController(
             LoginManager<Usuario, Modulo, Rol> loginManager,
            IHandlerExcepciones manejadorExcepciones) : base(manejadorExcepciones)
        {
            LoginManager = loginManager;
        }

      
        // POST  
        public async Task<HttpResponseMessage> Post(LoginModel loginModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    string result;

                    //1. Autentificacion  
                    loginModel.UsernameOrEmailAddress  = loginModel.UsernameOrEmailAddress.Trim();

                    var loginResult = await LoginManager.LoginAsync(loginModel.UsernameOrEmailAddress, loginModel.Password);

                    if (loginResult.Result == LoginResultType.InvalidUserNameOrEmailAddress
                        || loginResult.Result == LoginResultType.InvalidPassword)
                    {
                        Log.InfoFormat("No existe el usuario [{0}]", loginModel.UsernameOrEmailAddress);

                        result = string.Format("Usuario ó Clave incorrecta");
                        return Request.CreateResponse(HttpStatusCode.Unauthorized,
                            result, Configuration.Formatters.JsonFormatter);
                    }

                    if (loginResult.Result == LoginResultType.LockedOut)
                    {
                        Log.InfoFormat(string.Format("El usuario [{0}], se encuentra bloqueado/inactivo", loginModel.UsernameOrEmailAddress));
                        result =  string.Format("El usuario [{0}], se encuentra bloqueado/inactivo", loginModel.UsernameOrEmailAddress);

                        return Request.CreateResponse(HttpStatusCode.Unauthorized, result,
                            Configuration.Formatters.JsonFormatter);
                    }

                    if (loginResult.Result == LoginResultType.SucessPasswordResetCode)
                    {

                        result = string.Format("El usuario [{0}], requiere cambio de contraseña o existe un proceso de reseteo de contraseña", loginModel.UsernameOrEmailAddress);
 
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, result,
                           Configuration.Formatters.JsonFormatter);
                    }

                    if (loginResult.Result == LoginResultType.SucessAuthentication)
                    {

                        var usuario = loginResult.User;

                        var jwtToken = new JWTToken<Usuario>();
                        result = jwtToken.CreateToken(usuario);
 
                        var resp = new HttpResponseMessage(HttpStatusCode.OK);
                        resp.Content = new StringContent(result, System.Text.Encoding.UTF8,
                            "text/plain");
                        return resp;
                    }

                }
            }

            catch (Exception ex)
            {

                var result = ManejadorExcepciones.HandleException(ex);
                Log.ErrorFormat(result.Message);

                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                resp.Content = new StringContent(result.Message);
                return resp;
            }

            var msg = string.Format("Usuario ó Clave incorrecta");
            return Request.CreateResponse(HttpStatusCode.Unauthorized,
                msg, Configuration.Formatters.JsonFormatter);


        }
    }
}
