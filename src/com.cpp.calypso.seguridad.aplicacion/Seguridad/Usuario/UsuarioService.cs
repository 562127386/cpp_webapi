using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using Microsoft.AspNet.Identity;

namespace com.cpp.calypso.seguridad.aplicacion
{
    public class UsuarioService :
        AsyncBaseCrudAppService<Usuario, UsuarioDto, PagedAndFilteredResultRequestDto,CrearUsuarioDto>,
        IUsuarioService
    {

         
        private readonly IAuthentication authentication;
        private readonly IApplication application;
        private readonly IParametroService ParametroService;
        private IUsuarioRepository<Usuario> RepositoryUsuario;
        private readonly AspUserManager<Rol, Usuario, Modulo> UserManager;
        private readonly AspRoleManager<Rol, Usuario> RoleManager;
        private readonly BaseModuleManager<Modulo, Usuario> ModuleManager;
        private readonly IdentityEmailMessageService EmailService;
        private readonly ITemplateEngine TemplateEngine;
       private readonly IRolRepository<Rol> RepositoryRol;
        private IRolService RolService;

        public UsuarioService(
                 IAuthentication authentication,
                 IApplication application,
                 IParametroService parametroService,
                  IUsuarioRepository<Usuario> repositoryUsuario,
                  AspUserManager<Rol, Usuario, Modulo> userManager,
                  AspRoleManager<Rol, Usuario> roleManager,
                   BaseModuleManager<Modulo, Usuario> moduleManager,
                  IdentityEmailMessageService emailService,
                  ITemplateEngine templateEngine,
                 
                  IRolRepository<Rol> rolRepository,
                  IRolService rolService) : base(repositoryUsuario)
        {
            this.authentication = authentication;
            this.application = application;
            this.ParametroService = parametroService;
            this.RepositoryUsuario = repositoryUsuario;
            this.UserManager = userManager;
            this.RoleManager = roleManager;
            this.ModuleManager = moduleManager;
            this.EmailService = emailService;
            this.TemplateEngine = templateEngine;
            
            this.RepositoryRol = rolRepository;
            RolService = rolService;

        }

        public override async Task<UsuarioDto> Get(EntityDto<int> input)
        {
            var usuario = await UserManager.FindByIdAsync(input.Id);
        
            var output = MapToEntityDto(usuario);

            return output;
        }

        public async Task<UsuarioDto> Get(string cuenta)
        {
            var usuario = await UserManager.FindByNameAsync(cuenta);

            var output = MapToEntityDto(usuario);

            return output;
        }

      

        [UnitOfWork]
        public override async Task<UsuarioDto> Create(CrearUsuarioDto input)
        {
            //1. Crear Usuario
            Logger.DebugFormat("1. Set Values ");

            var user = input.MapTo<Usuario>();
            //var user = ObjectMapper.Map<Usuario>(input);
            
            user.Estado = EstadoUsuario.Activo;

            var passwordRandom = GeneratePassword();

            //Hash 
            user.Password = new PasswordHasher().HashPassword(passwordRandom);

            //Para obligar a cambiar el password.
            user.SetNewPasswordResetCode();

            //Roles
            user.Roles = new Collection<Rol>();
            foreach (var nombreRol in input.Roles)
            {
                var role = await RoleManager.GetRoleByNameAsync(nombreRol);
                user.Roles.Add(role);
            }

            //Modulos
            user.Modulos = new Collection<Modulo>();
            foreach (var nombre in input.Modulos)
            {
                var module = await ModuleManager.GetModuleByIdAsync(nombre);
                user.Modulos.Add(module);
            }

            Logger.DebugFormat("2. Create User");

            CheckErrors(await UserManager.CreateAsync(user));

           

            //2. Enviar Correo
            try
            {
                var modelBodyEmail = new NotificacionUsuarioCreado();
                modelBodyEmail.PasswordRestCode = user.PasswordResetCode;
                modelBodyEmail.Nombres = user.NombresCompletos;
                modelBodyEmail.Usuario = user.Cuenta;
                modelBodyEmail.Password = passwordRandom;


                var sistemaURL = ParametroService.GetValor<string>(CodigosParametros.PARAMETRO_SISTEMA_URL);
                modelBodyEmail.Enlace = string.Format("{0}/{1}", sistemaURL, "Acceso/Ingreso");

                Logger.DebugFormat("3. Process Template");
                var body = await TemplateEngine.Process(Constantes.TEMPLATE_SEGURIDAD_ENVIO_CORREO_CREACION_USUARIO, modelBodyEmail);

                var msg = new IdentityMessage();
                msg.Destination = user.Correo;
                msg.Subject = "Creación de Usuario";
                msg.Body = body;

                Logger.DebugFormat("4. Send Email");
                await EmailService.SendAsync(msg);
            }
            catch (Exception ex)
            {
                var result = ManejadorExcepciones.HandleException(ex);
            }

            Logger.DebugFormat("5. Return User");

            return MapToEntityDto(user);
        }

        public override async Task<UsuarioDto> Update(CrearUsuarioDto input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            if (input.Roles != null)
            {
                CheckErrors(await UserManager.SetRoles(user, input.Roles));
            }

            if (input.Modulos != null)
            {
                CheckErrors(await UserManager.SetModules(user, input.Modulos));
            }

            await UpdateInternal(user);

            return await Get(input);
        }

        private async Task UpdateInternal(Usuario user)
        {
            CheckErrors(await UserManager.UpdateAsync(user));

            //TODO: Un mecanismo desacoplado, se puede utilizar eventos de dominio.

            //Verificar si el usuario autentificado, se esta actualizado.
            //Si es el caso, se debe establecer el usuario en aplicacion.
            var userAutentificado = application.GetCurrentUser();
            if (userAutentificado.Id == user.Id)
            {

                var moduloAutentificado = application.GetCurrentModule();
                userAutentificado = MapTo(user, moduloAutentificado);
                application.SetCurrentUser(userAutentificado);
            }

            return;
        }

        private UsuarioAutentificado MapTo(Usuario usuario, ModuloAutentificado moduloAutentificado)
        {

            //Establecer Informacion 
            //1.
            var usuarioAutentificado = new UsuarioAutentificado();
            usuarioAutentificado.Id = usuario.Id;
            usuarioAutentificado.Cuenta = usuario.Cuenta;
            usuarioAutentificado.Correo = usuario.Correo;
            usuarioAutentificado.Apellidos = usuario.Apellidos;
            usuarioAutentificado.Identificacion = usuario.Identificacion;
            usuarioAutentificado.Nombres = usuario.Nombres;

            foreach (var rol in usuario.Roles)
            {
                var rolAutentificado = new RolAutentificado();
                rolAutentificado.Id = rol.Id;
                rolAutentificado.Codigo = rol.Codigo;
                rolAutentificado.EsAdministrador = rol.EsAdministrador;
                rolAutentificado.Nombre = rol.Nombre;

                usuarioAutentificado.Roles.Add(rolAutentificado);
            }
    
            usuarioAutentificado.Modulos.Add(moduloAutentificado);

            return usuarioAutentificado;
        }

        public override async Task<UsuarioDto> InsertOrUpdateAsync(CrearUsuarioDto input)
        {
            if (EqualityComparer<int>.Default.Equals(input.Id, default(int)))
            {
                return await Create(input);
            }
            else {
                return await Update(input);
            }
        }

        public virtual async Task<LoginResult<Usuario>> RecoverPasswordAsync(string correoElectronicoCuenta)
        {

            //1. Recuperar Usuario
            var user = await UserManager.FindByNameOrEmailAsync(correoElectronicoCuenta);
            if (user == null)
            {
                return new LoginResult<Usuario>(LoginResultType.InvalidUserNameOrEmailAddress);
            }

            var result =  await SendRecoverPasswordAsync(user);
            if (result.Result != LoginResultType.SucessPasswordResetCode)
                return result;
 
            try
            {
                //3. Enviar Mensaje..
                var modelBodyEmail = new RecoverPasswordDto();
                modelBodyEmail.PasswordRestCode = user.PasswordResetCode;
                modelBodyEmail.Nombres = user.NombresCompletos;
                modelBodyEmail.Usuario = user.Cuenta;

                var sistemaURL = ParametroService.GetValor<string>(CodigosParametros.PARAMETRO_SISTEMA_URL);
                modelBodyEmail.Enlace = string.Format("{0}/{1}/?code={2}", sistemaURL, "acceso/Reset", user.PasswordResetCode);

                var body = await TemplateEngine.Process(Constantes.TEMPLATE_SEGURIDAD_ENVIO_CORREO_RECUPERAR_CLAVE, modelBodyEmail);

                var msg = new IdentityMessage();
                msg.Destination = user.Correo;
                msg.Subject = "Recupera tu contraseña";
                msg.Body = body;
                await EmailService.SendAsync(msg);
            }
            catch (Exception ex)
            {
                ManejadorExcepciones.HandleException(ex);
            }

            return new LoginResult<Usuario>(LoginResultType.Success);
        }

        public virtual async Task<LoginResult<Usuario>> ResetPassword(int id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return new LoginResult<Usuario>(LoginResultType.InvalidUserNameOrEmailAddress);
            }

      
            var result = await SendRecoverPasswordAsync(user);
            if (result.Result != LoginResultType.SucessPasswordResetCode)
                return result;

            try
            {
                //3. Enviar Mensaje..
                var modelBodyEmail = new RecoverPasswordDto();
                modelBodyEmail.PasswordRestCode = user.PasswordResetCode;
                modelBodyEmail.Nombres = user.NombresCompletos;
                modelBodyEmail.Usuario = user.Cuenta;

                var sistemaURL = ParametroService.GetValor<string>(CodigosParametros.PARAMETRO_SISTEMA_URL);
                modelBodyEmail.Enlace = string.Format("{0}/{1}/?code={2}", sistemaURL, "acceso/Reset", user.PasswordResetCode);

                var body = await TemplateEngine.Process(Constantes.TEMPLATE_SEGURIDAD_ENVIO_CORREO_RESETEO_CLAVE, modelBodyEmail);

                var msg = new IdentityMessage();
                msg.Destination = user.Correo;
                msg.Subject = "Reseteo de contraseña";
                msg.Body = body;
                await EmailService.SendAsync(msg);
            }
            catch (Exception ex)
            {
                ManejadorExcepciones.HandleException(ex);
            }

            return new LoginResult<Usuario>(LoginResultType.Success);
        }

        private  async Task<LoginResult<Usuario>> SendRecoverPasswordAsync(Usuario user) {

          
            //2. Crear codigo de reseteo 
            user.SetNewPasswordResetCode();
            (await UserManager.UpdateAsync(user)).CheckErrors();


            return new LoginResult<Usuario>(LoginResultType.SucessPasswordResetCode, user);
        }

        [UnitOfWork]
        public virtual async Task<IdentityResult> ChangePassword(string password,string newPassword)
        {
            if (password == newPassword) {
                return IdentityResult.Failed(string.Format("La nueva contraseña debe ser diferente a la actual contraseña"));
            }

            var userAutentificado = application.GetCurrentUser();

            if (userAutentificado == null)
            {
                throw new GenericException(string.Format("Intento de actualizar contraseña  del usuario "),
                    "Seguridad: No se puede actualizar contraseña, el usuario no esta autentificado");
            }

            var user = await UserManager.FindByIdAsync(userAutentificado.Id);
            if (user == null)
            {
                return IdentityResult.Failed(string.Format("El usuario no se encuentra autentificado"));
            }

            var resultCambio = await UserManager.ChangePasswordAsync(user,password,newPassword);

            if (!resultCambio.Succeeded)
                return resultCambio;

            try
            {
                //3. Enviar Mensaje..
                var modelBodyEmail = new ChangePasswordDto();
                modelBodyEmail.Nombres = user.NombresCompletos;
                modelBodyEmail.Usuario = user.Cuenta;
                modelBodyEmail.Fecha = DateTime.Now;
                
                var body = await TemplateEngine.Process(Constantes.TEMPLATE_SEGURIDAD_ENVIO_CORREO_CAMBIO_CLAVE, modelBodyEmail);

                var msg = new IdentityMessage();
                msg.Destination = user.Correo;
                msg.Subject = "Cambio de contraseña";
                msg.Body = body;
                await EmailService.SendAsync(msg);
            }
            catch (Exception ex)
            {
                var result = ManejadorExcepciones.HandleException(ex);
            }

            return resultCambio;
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors();
        }

        protected virtual string GeneratePassword() {
           
            //TODO: Se puede colocar en configuraciones
            bool includeLowercase = true;
            bool includeUppercase = true;
            bool includeNumeric = true;
            bool includeSpecial = false;
            bool includeSpaces = false;
            int lengthOfPassword = 8;

            return  PasswordGenerator.GeneratePassword(includeLowercase, includeUppercase, includeNumeric, includeSpecial, includeSpaces, lengthOfPassword);

        }

        [UnitOfWork]
        public async Task<UsuarioDto> Update(MyUsuarioDto input)
        {
            //1. Verificar si el input, corresponde al usuario actual
            var userAutentificado = application.GetCurrentUser();
            if (userAutentificado.Id != input.Id) {
                throw new GenericException(string.Format("Intento de actualizar información del usuario autentificado : {0}, con información del usuario : {1}", userAutentificado.Id,input.Id),
                    "Seguridad: La información del usuario no corresponde al usuario autentificado");
            }
      
            var user = await UserManager.GetUserByIdAsync(input.Id);

            var existeCambioCorreo = false;
            if (user.Correo != input.Correo)
                existeCambioCorreo = true;

            //Mapper
            user = ObjectMapper.Map(input, user);
    

            if (existeCambioCorreo) {
                user.SetNewPasswordResetCode();
            }

            await UpdateInternal(user);

            //2. Verificar cambio de Correo. (Enviar Correo)
            if (existeCambioCorreo) {
                try
                {
                    //3. Enviar Mensaje..
                    var modelBodyEmail = new RecoverPasswordDto();
                    modelBodyEmail.PasswordRestCode = user.PasswordResetCode;
                    modelBodyEmail.Nombres = user.NombresCompletos;
                    modelBodyEmail.Usuario = user.Cuenta;

                    var sistemaURL = ParametroService.GetValor<string>(CodigosParametros.PARAMETRO_SISTEMA_URL);
                    modelBodyEmail.Enlace = string.Format("{0}/{1}/?code={2}", sistemaURL, "acceso/Reset", user.PasswordResetCode);

                    var body = await TemplateEngine.Process(Constantes.TEMPLATE_SEGURIDAD_ENVIO_CORREO_CAMBIO_CORREO, modelBodyEmail);

                    var msg = new IdentityMessage();
                    msg.Destination = user.Correo;
                    msg.Subject = "Cambio de correo electrónico / Reseteo de contraseña";
                    msg.Body = body;
                    await EmailService.SendAsync(msg);
                }
                catch (Exception ex)
                {
                    ManejadorExcepciones.HandleException(ex);
                }
            }

            //3. Actualizar 
            return await Get(input);
        }

      


         


    }
}
