using Abp.Domain.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Gestor de Usuarios
    /// </summary>
    public class UserManager : AspUserManager<Rol, Usuario, Modulo>
    {
        public UserManager(AspUserStore<Rol, Usuario> userStore, 
            AspRoleManager<Rol, Usuario> roleManager, 
            BaseModuleManager<Modulo, Usuario> moduleManager,
            IParametroManager parametroManager) :
            base(userStore, roleManager, moduleManager, parametroManager)
        {
        }
    }

    /// <summary>
    /// Extends <see cref="UserManager{TUser,TKey}"/> of ASP.NET Identity Framework.
    /// </summary>
    public abstract class AspUserManager<TRole, TUser, TModule>
        : UserManager<TUser, int>
        , IDomainService
        where TRole : AspRole<TUser>, new()
        where TUser : AspUser<TUser>
        where TModule : Modulo
    {
        private readonly AspRoleManager<TRole, TUser> RoleManager;
        private readonly BaseModuleManager<TModule, TUser> ModuleManager;
        private readonly IParametroManager ParametroManager;

        public  AspUserStore<TRole, TUser> AspStore { get; }

        public AspUserManager(
            AspUserStore<TRole, TUser> userStore,
            AspRoleManager<TRole, TUser> roleManager,
            BaseModuleManager<TModule, TUser> moduleManager,
            IParametroManager parametroManager) : base(userStore)
        {

            

            AspStore = userStore;
            this.RoleManager = roleManager;
            this.ModuleManager = moduleManager;
            this.ParametroManager = parametroManager;


            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(ParametroManager.GetValor<int>(CodigosParametros.PARAMETRO_SEGURIDAD_BLOQUEO_USUARIO_TIEMPO));
            MaxFailedAccessAttemptsBeforeLockout = ParametroManager.GetValor<int>(CodigosParametros.PARAMETRO_SEGURIDAD_BLOQUEO_USUARIO_INTENTOS);

       
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = ParametroManager.GetValor<int>(CodigosParametros.PARAMETRO_SEGURIDAD_CLAVE_MINIMO_LONGITUD),
                RequireNonLetterOrDigit = ParametroManager.GetValor<bool>(CodigosParametros.PARAMETRO_SEGURIDAD_CLAVE_REQUIERE_CARACTER_DIFERENTE_LETRA_DIGITO),
                RequireDigit = ParametroManager.GetValor<bool>(CodigosParametros.PARAMETRO_SEGURIDAD_CLAVE_REQUIERE_DIGITO),
                RequireLowercase = ParametroManager.GetValor<bool>(CodigosParametros.PARAMETRO_SEGURIDAD_CLAVE_REQUIERE_LETRA_MINUSCULA),
                RequireUppercase = ParametroManager.GetValor<bool>(CodigosParametros.PARAMETRO_SEGURIDAD_CLAVE_REQUIERE_LETRA_MAYUSCULA),
            };

        }


        public override async Task<IdentityResult> CreateAsync(TUser user)
        {
            var result = await CheckDuplicateUsernameOrEmailAddressAsync(user.Id, user.UserName, user.Correo);
            if (!result.Succeeded)
            {
                return result;
            }

            return await base.CreateAsync(user);
        }

      

        /// <summary>
        /// Gets a user by given id.
        /// Throws exception if no user found with given id.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>User</returns>
        /// <exception cref="AbpException">Throws exception if no user found with given id</exception>
        public virtual async Task<TUser> GetUserByIdAsync(int userId)
        {
            var user = await FindByIdAsync(userId);
            if (user == null)
            {
                throw new  Exception("There is no user with id: " + userId);
            }

            return user;
        }

      
        public async override Task<IdentityResult> UpdateAsync(TUser user)
        {
            var result = await CheckDuplicateUsernameOrEmailAddressAsync(user.Id, user.UserName, user.Correo);
            if (!result.Succeeded)
            {
                return result;
            }
  
            return await base.UpdateAsync(user);
        }

        public async override Task<IdentityResult> DeleteAsync(TUser user)
        {
            return await base.DeleteAsync(user);
        }


        public virtual async Task<IdentityResult> CheckDuplicateUsernameOrEmailAddressAsync(long? expectedUserId, string userName, string emailAddress)
        {
            var user = (await FindByNameAsync(userName));
            if (user != null && user.Id != expectedUserId)
            {
                return IdentityResult.Failed(string.Format("El usuario {0}, ya existe", userName));
            }

            user = (await FindByEmailAsync(emailAddress));
            if (user != null && user.Id != expectedUserId)
            {
                return IdentityResult.Failed(string.Format("El correo {0}, ya se encuentra asignado a otro usuario", emailAddress));
            }

            return IdentityResult.Success;
        }

        public virtual async Task<IdentityResult> ChangePasswordAsync(TUser user, string currentPassword, string newPassword)
        {
 
            var result = await PasswordValidator.ValidateAsync(newPassword);
            if (!result.Succeeded)
            {
                return result;
            }

            var verificationResult = PasswordHasher.VerifyHashedPassword(user.Password, currentPassword);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return IdentityResult.Failed(string.Format("La actual contraseña no es valida"));
            }

            await AspStore.SetPasswordHashAsync(user, PasswordHasher.HashPassword(newPassword));

            return await base.UpdateAsync(user);
             
        }

        /// <summary>
        /// Cambiar la clave
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public virtual async Task<IdentityResult> ChangePasswordAsync(TUser user, string newPassword)
        {
            var result = await PasswordValidator.ValidateAsync(newPassword);
            if (!result.Succeeded)
            {
                return result;
            }

            await AspStore.SetPasswordHashAsync(user, PasswordHasher.HashPassword(newPassword));

            return await base.UpdateAsync(user);
        }


        public async override  Task<ClaimsIdentity> CreateIdentityAsync(TUser user, string authenticationType)
        {
            var identity = await base.CreateIdentityAsync(user, authenticationType);

            //TODO: Obtener como recuperar el modulo autentificado desde el usuario
            //Agregar Claim. (Modulo)....
            //if (user.Modulo != null)
            //{
            //    identity.AddClaim(new Claim(BaseClaimTypes.ModuloId, user.Modulo.Id.ToString(CultureInfo.InvariantCulture)));
            //}

            return identity;

        }

        public async Task<IdentityResult> SetModules(TUser user, int[] modulos)
        {
            //Remove from removed modules
            foreach (var moduleUser in user.Modulos.ToList())
            {
                var module = await ModuleManager.FindByIdAsync(moduleUser.Id);
                if (modulos.All(moduleId => module.Id  != moduleId))
                {
                    var result = await RemoveFromModuleAsync(user, module);
                    if (!result.Succeeded)
                    {
                        return result;
                    }
                }
            }

            //Add to added modules
            foreach (var moduleUser in modulos)
            {
                var module = await ModuleManager.FindByIdAsync(moduleUser);
                if (user.Modulos.All(ur => ur.Id != module.Id))
                {
                    var result = await AddToModuleAsync(user, module);
                    if (!result.Succeeded)
                    {
                        return result;
                    }
                }
            }

            return IdentityResult.Success;
        }

        public virtual async Task<IdentityResult> AddToModuleAsync(TUser user, TModule module)
        {
            return await Task.Run(() =>
            {
                user.Modulos.Add(module);

                return IdentityResult.Success;
            });
           
        }

        public virtual async Task<IdentityResult> RemoveFromModuleAsync(TUser user, TModule module)
        {
            return await Task.Run(() =>
            {
                var moduleUser = user.Modulos.Where(r => r.Nombre == module.Nombre).SingleOrDefault();
                if (moduleUser == null)
                {
                    return IdentityResult.Success;
                }
                user.Modulos.Remove(moduleUser);

                return IdentityResult.Success;
            });
            
        }


        /// <summary>
        /// Buscar un usuario, por el codigo de reseteo de contrasena
        /// </summary>
        /// <param name="resetCode"></param>
        /// <returns></returns>
        public virtual async Task<TUser> FindByPasswordResetCodeAsync(string resetCode)
        {
            return await AspStore.FindByPasswordResetCodeAsync(resetCode); 
        }


        /// <summary>
        /// Buscar un usuario, por el nombre de la cuenta o por correo electronico
        /// </summary>
        /// <param name="userNameOrEmailAddress"></param>
        /// <returns></returns>
        public virtual async Task<TUser> FindByNameOrEmailAsync(string userNameOrEmailAddress)
        {
            return await AspStore.FindByNameOrEmailAsync(userNameOrEmailAddress);
        }


        public virtual async Task<IdentityResult> SetRoles(TUser user, string[] roleNames)
        {
            //Remove from removed roles
            foreach (var userRole in user.Roles.ToList())
            {
                var role = await RoleManager.FindByIdAsync(userRole.Id);
                if (roleNames.All(roleName => role.Name != roleName))
                {
                    var result = await RemoveFromRoleAsync(user, role);
                    if (!result.Succeeded)
                    {
                        return result;
                    }
                }
            }

            //Add to added roles
            foreach (var roleName in roleNames)
            {
                var role = await RoleManager.GetRoleByNameAsync(roleName);
                if (user.Roles.All(ur => ur.Nombre != roleName))
                {
                    var result = await AddToRoleAsync(user, role);
                    if (!result.Succeeded)
                    {
                        return result;
                    }
                }
            }

            return IdentityResult.Success;
        }

        

        public virtual async Task<IdentityResult> AddToRoleAsync(TUser user, TRole rol)
        {
            return await Task.Run(() =>
            {

                //TODO: El parametro generico de la clase, puede ser diferente al tipo Rol 
                user.Roles.Add(rol as Rol);
                return IdentityResult.Success;

            });
        }

        public virtual async Task<IdentityResult> RemoveFromRoleAsync(TUser user, TRole rol)
        {
            return await Task.Run(() =>
            {

                var rolUser = user.Roles.Where(r => r.Nombre == rol.Nombre).SingleOrDefault();
                if (rolUser == null)
                {
                    return IdentityResult.Success;
                }
                user.Roles.Remove(rolUser);

                return IdentityResult.Success;
            });
        }

    }
}
