using Abp.Application.Services.Dto;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using com.cpp.calypso.seguridad.aplicacion;
using CommonServiceLocator;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace com.cpp.calypso.web
{
    [AllowAnonymous]
    public class AccesoController : BaseController
    {

        //Logger de la aplicacion
        private static readonly ILogger Log =
            ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(AccesoController));


        private readonly IUsuarioService usuarioService;
        private readonly IModuloService moduloService;
        private IApplication Application;

        private readonly LoginManager<Usuario, Modulo, Rol> LoginManager;
        private readonly AspUserManager<Rol, Usuario, Modulo> userManager;
        
        private readonly IAuthenticationManager authenticationManager;
         
        public AccesoController(
            IHandlerExcepciones manejadorExcepciones,
            IApplication application,
            LoginManager<Usuario, Modulo, Rol> loginManager,
            AspUserManager<Rol, Usuario, Modulo> userManager,
            IUsuarioService usuarioService,
            IModuloService moduloService,       
            IAuthenticationManager authenticationManager
             
            )
            : base(manejadorExcepciones)
        {

            Application = application;

            this.LoginManager = loginManager;

            this.userManager = userManager;


            this.authenticationManager = authenticationManager;
            
            this.usuarioService = usuarioService;

            this.moduloService = moduloService;
        }


        public ActionResult Ingreso(string msg, string nombreUsuario)
        {

            ViewBag.Usuario = String.Empty;
            if (!String.IsNullOrEmpty(nombreUsuario))
            {
                ViewBag.Usuario = HttpUtility.UrlDecode(nombreUsuario);
            }

            ViewBag.Mensaje = msg;

            var model = new IngresoViewModel();

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Ingreso(IngresoViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    //1. Autentificacion  
                    model.Usuario = model.Usuario.Trim();

                    var loginResult = await LoginManager.LoginAsync(model.Usuario, model.Password);

                    if (loginResult.Result == LoginResultType.InvalidUserNameOrEmailAddress
                        || loginResult.Result == LoginResultType.InvalidPassword)
                    {
                        Log.InfoFormat("No existe el usuario [{0}]", model.Usuario);

                        ModelState.AddModelError("", string.Format("Usuario ó Clave incorrecta"));

                        //Setear nuevamente el campo usuario
                        ViewBag.Usuario = model.Usuario;
                        return IntentarIngreso(model);
                    }

                    if (loginResult.Result == LoginResultType.LockedOut)
                    {
                        Log.InfoFormat(string.Format("El usuario [{0}], se encuentra bloqueado/inactivo", model.Usuario));
                        ModelState.AddModelError("", string.Format("El usuario [{0}], se encuentra bloqueado/inactivo", model.Usuario));
                        return IntentarIngreso(model);
                    }

                    if (loginResult.Result == LoginResultType.SucessPasswordResetCode)
                    {

                        //Guardar en sesion, la cuenta que ya sido autentificada
                        ManejadorSessionAcceso.setSession(ManejadorSessionAcceso.EnumerableSessionAcceso.usuario_id_pasado_autentificacion,
                            loginResult.User.Id);


                        return RedirectToAction("CambiarClave");
                    }

                    if (loginResult.Result == LoginResultType.SucessAuthentication)
                    {

                        var usuario = loginResult.User;

                        //Si unicamente existe un modulo
                        if (usuario.Modulos.Count == 1)
                        {
                            var moduloDefault = usuario.Modulos.FirstOrDefault();

                            var mensaje =
                            string.Format("El usuario {0} ingresa por defecto con el codigo de modulo {1} , fecha y hora: {2}", usuario, moduloDefault.Codigo
                            , Application.getDateTime());

                            return await AutentificarUsuario(usuario, moduloDefault);
                        }
                        else if (usuario.Modulos.Count > 1)
                        {
                            Log.InfoFormat(string.Format("El usuario [{0}], posee varios modulos ", model.Usuario));

                            //Guardar en sesion, la cuenta que ya sido autentificada
                            ManejadorSessionAcceso.setSession(ManejadorSessionAcceso.EnumerableSessionAcceso.usuario_id_pasado_autentificacion,
                                usuario.Id);

                            return RedirectToAction("SeleccionarModulo");


                        }
                        else if (usuario.Modulos.Count == 0)
                        {
                            Log.ErrorFormat("Sistema. El usuario no posee modulos asociadas");
                            ModelState.AddModelError(string.Empty, "El usuario no posee modulos asociadas");
                        }
                    }

                    Log.InfoFormat("Sistema. El usuario {0} no existe", model.Usuario);
                    ModelState.AddModelError(string.Empty, string.Format("El usuario no existe. {0}", loginResult.Result.ToString()));

                }
            }

            catch (Exception ex)
            {

                var result = ManejadorExcepciones.HandleException(ex);
                Log.ErrorFormat(result.Message);
                ModelState.AddModelError("", result.Message);
            }

            return View(model);
        }



        private ActionResult IntentarIngreso(IngresoViewModel model)
        {
            return View("Ingreso", model);
        }

        [HttpGet]
        public async Task<ActionResult> SeleccionarModulo()
        {
            int? usuarioId = ManejadorSessionAcceso.getSession(ManejadorSessionAcceso.EnumerableSessionAcceso.usuario_id_pasado_autentificacion) as int?;

            if (usuarioId.HasValue && usuarioId.Value > 0)
            {
                var usuario = await usuarioService.Get(new EntityDto<int>(usuarioId.Value));
                return View(usuario);
            }

            return RedirectToAction("Ingreso", new { msg = "Ingrese sus credenciales" });
        }

        [HttpGet]
        public async Task<ActionResult> CambiarClave()
        {

            int? usuarioId = ManejadorSessionAcceso.getSession(ManejadorSessionAcceso.EnumerableSessionAcceso.usuario_id_pasado_autentificacion) as int?;

            if (usuarioId.HasValue && usuarioId.Value > 0)
            {
                var usuario = await usuarioService.Get(new EntityDto<int>(usuarioId.Value));

                if (usuario != null)
                {
                    var model = new NuevaClaveViewModel();
                    model.NombreUsuario = usuario.NombresCompletos();

                    return View(model);
                }
            }

            return RedirectToAction("Ingreso", new { msg = "Ingrese sus credenciales" });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CambiarClave(NuevaClaveViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int? usuarioId = ManejadorSessionAcceso.getSession(ManejadorSessionAcceso.EnumerableSessionAcceso.usuario_id_pasado_autentificacion) as int?;
                    if (usuarioId.HasValue && usuarioId.Value > 0)
                    {

                        //Cambiar Clave
                        var loginResult = await LoginManager.ChangePasswordAsync(usuarioId.Value, model.Password);

                        if (loginResult.Result == LoginResultType.SucessAuthentication)
                        {

                            var usuario = loginResult.User;

                            //Si unicamente existe un modulo
                            if (usuario.Modulos.Count == 1)
                            {
                                var moduloDefault = usuario.Modulos.FirstOrDefault();

                                var mensaje =
                                string.Format("El usuario {0} ingresa por defecto con el codigo de modulo {1} , fecha y hora: {2}", usuario, moduloDefault.Codigo
                                , Application.getDateTime());

                                return await AutentificarUsuario(usuario, moduloDefault);
                            }
                            else if (usuario.Modulos.Count > 1)
                            {
                                Log.InfoFormat(string.Format("El usuario [{0}], posee varios modulos ", loginResult.User.Cuenta));

                                //Guardar en sesion, la cuenta que ya sido autentificada
                                ManejadorSessionAcceso.setSession(ManejadorSessionAcceso.EnumerableSessionAcceso.usuario_id_pasado_autentificacion,
                                    usuario.Id);

                                return RedirectToAction("SeleccionarModulo");


                            }
                            else if (usuario.Modulos.Count == 0)
                            {
                                Log.ErrorFormat("Sistema. El usuario no posee modulos asociadas");
                                ModelState.AddModelError(string.Empty, "El usuario no posee modulos asociadas");
                            }
                        }

                        ModelState.AddModelError("", loginResult.Result.ToString());
                    }
                    else
                    {

                        return RedirectToAction("Ingreso", new { msg = "Ingrese sus credenciales" });
                    }
                }
            }
            catch (Exception ex)
            {
                var result = ManejadorExcepciones.HandleException(ex);
                Log.ErrorFormat(result.Message);
                ModelState.AddModelError("", result.Message);
            }


            return View(model);
        }



        [HttpGet]
        public ActionResult RecuperarClave()
        {
            var model = new RecuperarClaveViewModel();

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RecuperarClave(RecuperarClaveViewModel model)
        {
            if (ModelState.IsValid)
            {

                //Recuperar Clave
                var loginResult = await usuarioService.RecoverPasswordAsync(model.CorreoElectronicoCuenta);

                if (loginResult.Result == LoginResultType.SucessPasswordResetCode)
                {
                    return RedirectToAction("Ingreso", new { msg = "Se envió un mensaje al correo electrónico registrado,con las instrucciones para recuperar la contraseña" });
                }


                ModelState.AddModelError("", loginResult.Result.ToString());
            }
            return View(model);
        }


        
        [HttpGet]
        //[Route("Reset/{resetCode}")]
        [Route("Reset/{code:alpha}")]
        public async Task<ActionResult> Reset(string code)
        {
            //1. Verificar correo de reseteo
            var loginResult = await LoginManager.ValidatePasswordResetCode(code);

            if (loginResult.Result == LoginResultType.SucessPasswordResetCode)
            {
                //Por seguridad cerrar la sesion actual, evitar cruces de usuarios
                authenticationManager.SignOut();
                Session.Clear();

                //Guardar en sesion, la cuenta que ya sido autentificada parcialmente
                ManejadorSessionAcceso.setSession(ManejadorSessionAcceso.EnumerableSessionAcceso.usuario_id_pasado_autentificacion,
                    loginResult.User.Id);

                return RedirectToAction("CambiarClave");
            }

            return RedirectToAction("Ingreso", new { msg = "El código es inválido o ha caducado. Ingrese sus credenciales" });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SeleccionarModulo(string codigoModulo)
        {
            int? usuarioId = ManejadorSessionAcceso.getSession(ManejadorSessionAcceso.EnumerableSessionAcceso.usuario_id_pasado_autentificacion) as int?;
            if (usuarioId.HasValue && usuarioId.Value > 0 && !string.IsNullOrWhiteSpace(codigoModulo))
            {
                //Verificar q la cuenta no este asignada a otro usuario
                var usuario = await usuarioService.Get(new EntityDto<int>(usuarioId.Value));

                if (usuario != null)
                {

                    // El usuario tiene el modulo asociado 
                    var modulo = usuario.Modulos.Where(c => c.Codigo == codigoModulo).SingleOrDefault();
                    if (modulo == null)
                    {
                        string error = string.Format("El usuario {0}, no tiene asociado el modulo con el codigo {1}", usuario.Cuenta, codigoModulo);
                        throw new GenericException(error, error);
                    }

                    return await AutentificarUsuario(usuario, modulo);
                }

                Log.InfoFormat("El usuario con Identificador  {0}, no existe", usuarioId.Value);
            }

            return RedirectToAction("Ingreso", new { msg = "El usuario o el módulo seleccionado no esta asociado" });
        }


        private async Task<ActionResult> AutentificarUsuario(Usuario usuario, Modulo modulo)
        {
            try
            {
                var accessResult = await LoginManager.AccessAsync(usuario, modulo);
                return SignIn(accessResult);
            }
            catch (Exception ex)
            {
                var result = ManejadorExcepciones.HandleException(ex);

                authenticationManager.SignOut();

                Session.Clear();

                return Redirect(Constantes.AUTENTIFICACION_LOGIN);
            }

        }


        /// <summary>
        ///  Autentificar usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="codigoModulo"></param>
        /// <returns></returns>
        private async Task<ActionResult> AutentificarUsuario(UsuarioDto usuario, ModuloDto modulo)
        {
            try
            {
                var accessResult = await LoginManager.AccessAsync(usuario.Id, modulo.Id);
                return SignIn(accessResult);
            }
            catch (Exception ex)
            {
                var result = ManejadorExcepciones.HandleException(ex);

                authenticationManager.SignOut();

                Session.Clear();

                return Redirect(Constantes.AUTENTIFICACION_LOGIN);
            }
        }


        private  ActionResult SignIn(LoginResult<Usuario>  accessResult) {

            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            // Many browsers do not clean up session cookies when you close them. So the rule of thumb must be:
            // For having a consistent behaviour across all browsers, don't rely solely on browser behaviour for proper clean-up
            // of session cookies. It is safer to use non-session cookies (IsPersistent == true) in bundle with an expiration date.
            // See http://blog.petersondave.com/cookies/Session-Cookies-in-Chrome-Firefox-and-Sitecore/

            authenticationManager.SignIn(
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(int.Parse(System.Configuration.ConfigurationManager.AppSettings["AuthSession.ExpireTimeInMinutes.WhenNotPersistent"] ?? "30"))
                },
                accessResult.Identity);


            //direccionar a URL Default
            string URL = Request.ApplicationPath;
            return Redirect(URL);
        }

        public ActionResult Salir()
        {
            try
            {
                authenticationManager.SignOut();
            }
            catch (Exception ex)
            {
                var result = ManejadorExcepciones.HandleException(ex);
            }

            authenticationManager.SignOut();
            Session.Clear();

            return Redirect(Constantes.AUTENTIFICACION_LOGIN);

        }

        public ActionResult NoAutorizado(string reason)
        {

            ViewBag.reason = reason;
            return View();
        }

    }
}