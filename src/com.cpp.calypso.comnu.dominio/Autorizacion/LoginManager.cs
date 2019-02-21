using Abp.Auditing;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Extensions;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Gestion de Login de Usuarios
    /// </summary>
    public class LoginManager<TUser, TModule, TRole> : IDomainService, ITransientDependency
         where TRole : AspRole<TUser>, new()
         where TModule : Modulo
         where TUser : AspUser<TUser>
    {
        public IClientInfoProvider ClientInfoProvider { get; set; }

        private readonly IApplication application;
        private readonly AspUserManager<TRole, TUser, TModule> UserManager;
        private readonly BaseModuleManager<TModule, TUser> ModuleManager;
        private readonly IUnitOfWorkManager UnitOfWorkManager;
        private readonly IBaseRepository<Sesion> SessionRepository;

        private readonly IPasswordManager<TUser> PasswordManager;

        public LoginManager(
            IApplication application,
            AspUserManager<TRole, TUser, TModule> userManager,
            BaseModuleManager<TModule, TUser> moduleManager,
            IUnitOfWorkManager unitOfWorkManager,
            IBaseRepository<Sesion> sessionRepository,
            IPasswordManager<TUser> passwordManager)
        {
            this.application = application;
            this.UserManager = userManager;
            this.ModuleManager = moduleManager;
            this.UnitOfWorkManager = unitOfWorkManager;
            this.SessionRepository = sessionRepository;
            ClientInfoProvider = NullClientInfoProvider.Instance;
            this.PasswordManager = passwordManager;
        }


        /// <summary>
        /// Login de un usuario. (Usuario y Clave)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="plainPassword"></param>
        /// <param name="shouldLockout"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<LoginResult<TUser>> LoginAsync(string userName, string plainPassword, bool shouldLockout = true)
        {
            var result = await LoginAsyncInternal(userName, plainPassword, shouldLockout);
            await SaveLoginSession(result, null, userName);
            return result;
        }

        [UnitOfWork]
        public virtual async Task<LoginResult<TUser>> AccessAsync(int usuarioId, int moduloId)
        {
            var user = await UserManager.FindByIdAsync(usuarioId);
            if (user == null)
            {
                return new LoginResult<TUser>(LoginResultType.InvalidUserNameOrEmailAddress);
            }

            var modulo = await ModuleManager.FindByIdAsync(moduloId);
            if (modulo == null)
            {
                return new LoginResult<TUser>(LoginResultType.InvalidUserNameOrEmailAddress);
            }

            return await AccessAsync(user, modulo);
        }

        /// <summary>
        /// Acceso. (Usuario valido, y seleccionado Modulo)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="moduloId"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<LoginResult<TUser>> AccessAsync(TUser usuario, TModule modulo)
        {
            var result = await AccessAsyncInternal(usuario, modulo);
            await SaveLoginSession(result, modulo, usuario.Cuenta);

            var usuarioAutentificado = MapTo(usuario, modulo);

            application.SetCurrentUser(usuarioAutentificado);

            application.SetCurrentModule(usuarioAutentificado.Modulos.ToList()[0]);

            return result;
        }

        private UsuarioAutentificado MapTo(TUser usuario, TModule modulo)
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

            var moduloAutentificado = new ModuloAutentificado();
            moduloAutentificado.Id = modulo.Id;
            moduloAutentificado.Codigo = modulo.Codigo;
            moduloAutentificado.Nombre = modulo.Nombre;
            usuarioAutentificado.Modulos.Add(moduloAutentificado);

            return usuarioAutentificado;
        }

        protected virtual async Task<LoginResult<TUser>> AccessAsyncInternal(TUser usuario, TModule modulo)
        {

            return new LoginResult<TUser>(
                modulo,
                usuario,
                await UserManager.CreateIdentityAsync(usuario, DefaultAuthenticationTypes.ApplicationCookie)
            );

        }

        protected virtual async Task<LoginResult<TUser>> LoginAsyncInternal(string userName, string plainPassword, bool shouldLockout)
        {
            if (userName.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(userName));
            }

            if (plainPassword.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(plainPassword));
            }

            //1. Verificar si existe Usuario
            var user = await UserManager.FindByNameAsync(userName);
            if (user == null)
            {
                return new LoginResult<TUser>(LoginResultType.InvalidUserNameOrEmailAddress);
            }

            //2 Verificar si existe un bloqueo.
            if (await UserManager.IsLockedOutAsync(user.Id))
            {
                return new LoginResult<TUser>(LoginResultType.LockedOut, user);
            }

            //3. Verificar Clave. 
            var verificationResult = await PasswordManager.ValidateCredentials(user, plainPassword);

            //(Realizar bloqueo por intentos fallidos)
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return await GetFailedPasswordValidationAsLoginResultAsync(user, shouldLockout);
            }

            if (verificationResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                return await GetSuccessRehashNeededAsLoginResultAsync(user);
            }

            await UserManager.ResetAccessFailedCountAsync(user.Id);

            //4. Existe codigo de reseteo de Clave
            if (!string.IsNullOrWhiteSpace(await user.GetPasswordResetCode(user)))
            {

                return new LoginResult<TUser>(LoginResultType.SucessPasswordResetCode, user);
            }


            return new LoginResult<TUser>(LoginResultType.SucessAuthentication, user);
        }

        protected virtual async Task<LoginResult<TUser>> GetFailedPasswordValidationAsLoginResultAsync(TUser user, bool shouldLockout = false)
        {
            if (shouldLockout)
            {
                if (await TryLockOutAsync(user.Id))
                {
                    return new LoginResult<TUser>(LoginResultType.LockedOut, user);
                }
            }

            return new LoginResult<TUser>(LoginResultType.InvalidPassword, user);
        }

        protected virtual async Task<LoginResult<TUser>> GetSuccessRehashNeededAsLoginResultAsync(TUser user, bool shouldLockout = false)
        {
            return await GetFailedPasswordValidationAsLoginResultAsync(user, shouldLockout);
        }

        protected virtual async Task<bool> TryLockOutAsync(int userId)
        {
            using (var uow = UnitOfWorkManager.Begin(TransactionScopeOption.Suppress))
            {
                (await UserManager.AccessFailedAsync(userId)).CheckErrors();

                var isLockOut = await UserManager.IsLockedOutAsync(userId);

                await UnitOfWorkManager.Current.SaveChangesAsync();

                await uow.CompleteAsync();

                return isLockOut;
            }
        }

        /// <summary>
        /// Guardar informacion de la session.
        /// </summary>
        /// <param name="loginResult"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        protected virtual async Task SaveLoginSession(LoginResult<TUser> loginResult, TModule module, string userName)
        {
            using (var uow = UnitOfWorkManager.Begin(TransactionScopeOption.Suppress))
            {
                //Guardar Session
                var sesion = new Sesion
                {
                    UsuarioId = loginResult.User != null ? loginResult.User.Id : (int?)null,
                    Cuenta = userName,
                    Result = loginResult.Result,
                    ModuloId = loginResult.User != null && module != null ? module.Id : (int?)null,

                    BrowserInfo = ClientInfoProvider.BrowserInfo,
                    ClientIpAddress = ClientInfoProvider.ClientIpAddress,
                    ClientName = ClientInfoProvider.ComputerName,
                };

                await SessionRepository.InsertAsync(sesion);
                await UnitOfWorkManager.Current.SaveChangesAsync();

                await uow.CompleteAsync();
            }
        }

        /// <summary>
        /// Cambiar la clave del usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task<LoginResult<TUser>> ChangePasswordAsync(int usuarioId, string password)
        {

            //Recuperar usuario
            var usuario = await UserManager.FindByIdAsync(usuarioId);
            if (usuario == null)
            {
                return new LoginResult<TUser>(LoginResultType.InvalidUserNameOrEmailAddress);
            }


            //1. Cambiar Clave
            (await UserManager.ChangePasswordAsync(usuario, password)).CheckErrors();

            //2. Clear Code Reseteo
            usuario.ClearPasswordResetCode(usuario);

            //3. Guardar y verificar errores
            (await UserManager.UpdateAsync(usuario)).CheckErrors();

            return new LoginResult<TUser>(LoginResultType.SucessAuthentication, usuario);
        }

        [UnitOfWork]
        public virtual async Task<LoginResult<TUser>> ValidatePasswordResetCode(string resetCode)
        {
            var user = await UserManager.FindByPasswordResetCodeAsync(resetCode);
            if (user == null)
            {
                return new LoginResult<TUser>(LoginResultType.InvalidUserNameOrEmailAddress);
            }

            return new LoginResult<TUser>(LoginResultType.SucessPasswordResetCode, user);
        }


    }
}
