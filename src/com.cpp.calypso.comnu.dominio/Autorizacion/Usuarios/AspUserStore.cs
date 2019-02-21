using Abp.Domain.Uow;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.cpp.calypso.comun.dominio
{

    public class UserStore : AspUserStore<Rol, Usuario>
    {
        public UserStore(IBaseRepository<Usuario> userRepository) :
            base(userRepository)
        {

        }
    }

    /// <summary>
    /// Implements 'User Store' of ASP.NET Identity Framework.
    /// </summary>
    public abstract class AspUserStore<TRole, TUser> :
        IUserStore<TUser, int>,
        IUserPasswordStore<TUser, int>,
        IUserEmailStore<TUser, int>,
        IUserLockoutStore<TUser, int>,
        IUserSecurityStampStore<TUser, int>,
        IUserRoleStore<TUser, int>,
        IQueryableUserStore<TUser, int>

        where TRole : AspRole<TUser>
        where TUser : AspUser<TUser>
    {
        private readonly IBaseRepository<TUser> UserRepository;

        public AspUserStore(IBaseRepository<TUser> userRepository)
        {
            this.UserRepository = userRepository;
        }

        #region IQueryableUserStore

        public virtual IQueryable<TUser> Users => UserRepository.GetAll();

        #endregion


        #region IUserStore

        public virtual async Task CreateAsync(TUser user)
        {
            await UserRepository.InsertAsync(user);
        }

        public virtual async Task UpdateAsync(TUser user)
        {
            await UserRepository.UpdateAsync(user);
        }

        public virtual async Task DeleteAsync(TUser user)
        {
            await UserRepository.DeleteAsync(user.Id);
        }

        public virtual  Task<TUser> FindByIdAsync(int userId)
        {
            var user = UserRepository.Get(userId, u => u.Roles, u => u.Modulos);
            return Task.FromResult(user);
 
        }

        public virtual  Task<TUser> FindByNameAsync(string userName)
        {
            var user = UserRepository.GetAllIncluding(u => u.Roles, u => u.Modulos)
                .Where(u => u.Cuenta == userName).FirstOrDefault();

            return Task.FromResult(user);
        }

        public virtual   Task<TUser> FindByEmailAsync(string email)
        {
            var user = UserRepository.GetAllIncluding(u => u.Roles, u => u.Modulos)
                .Where(u => u.Correo == email).FirstOrDefault();

            return Task.FromResult(user);

        }

        public virtual  Task<TUser> FindByPasswordResetCodeAsync(string resetCode)
        {
            var user = UserRepository.GetAllIncluding(u => u.Roles, u => u.Modulos)
              .Where(u => u.PasswordResetCode == resetCode).FirstOrDefault();

            return Task.FromResult(user);

        }

        public virtual  Task<TUser> FindByNameOrEmailAsync(string userNameOrEmailAddress)
        {
            var user = UserRepository.GetAllIncluding(u => u.Roles, u => u.Modulos)
                .Where(u => u.Cuenta == userNameOrEmailAddress || u.Correo == userNameOrEmailAddress).FirstOrDefault();

            return Task.FromResult(user);
        }

        #endregion


        #region IUserRoleStore

        public virtual  Task AddToRoleAsync(TUser user, string roleName)
        {
            var rol = user.Roles.Where(r => r.Name == roleName).SingleOrDefault();
            user.Roles.Add(rol);

            return Task.FromResult(0);
        }

        public virtual  Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            var rol = user.Roles.Where(r => r.Name == roleName).SingleOrDefault();
            if (rol == null)
            {
                return Task.FromResult(0);
            }
            user.Roles.Remove(rol);

            return Task.FromResult(0);
        }

        [UnitOfWork]
        public virtual  Task<IList<string>> GetRolesAsync(TUser user)
        {
            var query = from userRole in user.Roles
                        select userRole.Name
                        ;
            var list = query.ToList();
            return Task.FromResult(list as IList<string>); 
        }

        public virtual  Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            var rol = user.Roles.Where(r => r.Name == roleName).SingleOrDefault();
            if (rol == null)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        #endregion


        #region IUserPasswordStore

        public virtual Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            user.Password = passwordHash;
            return Task.FromResult(0);
        }

        public virtual Task<string> GetPasswordHashAsync(TUser user)
        {
            return Task.FromResult(user.Password);
        }

        public virtual Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.Password));
        }

        #endregion


        #region IUserEmailStore

        public virtual Task SetEmailAsync(TUser user, string email)
        {
            user.Correo = email;
            return Task.FromResult(0);
        }

        public virtual Task<string> GetEmailAsync(TUser user)
        {
            return Task.FromResult(user.Correo);
        }

        public virtual Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            //Proceso de confirmacion de correo no se implementa. Siempre sera confirmado el correo.
            return Task.FromResult(true);
        }

        public virtual Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            //Proceso de confirmacion de correo no se implementa. Siempre sera confirmado el correo.
            return Task.FromResult(true);
        }

        #endregion


        #region IUserLockoutStore

        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            return Task.FromResult(
                user.FechaFinalizacionBloqueUtc.HasValue
                    ? new DateTimeOffset(DateTime.SpecifyKind(user.FechaFinalizacionBloqueUtc.Value, DateTimeKind.Utc))
                    : new DateTimeOffset()
            );
        }

        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            user.FechaFinalizacionBloqueUtc = lockoutEnd == DateTimeOffset.MinValue ? new DateTime?() : lockoutEnd.UtcDateTime;
            return Task.FromResult(0);
        }

        public Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            return Task.FromResult(++user.CantidadAccesoFallido);
        }

        public Task ResetAccessFailedCountAsync(TUser user)
        {
            user.CantidadAccesoFallido = 0;
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            return Task.FromResult(user.CantidadAccesoFallido);
        }

        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            return Task.FromResult(user.BloqueoHabilitado);
        }

        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            user.BloqueoHabilitado = enabled;
            return Task.FromResult(0);
        }

        #endregion


        #region IUserSecurityStampStore

        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            user.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(TUser user)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        #endregion


        #region IDisposable

        public virtual void Dispose()
        {
            //No need to dispose since using IOC.
        }

        #endregion
    }
}
