using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Gestor de Roles
    /// </summary>
    public class RoleManager : AspRoleManager<Rol, Usuario>
    {
        public RoleManager(AspRoleStore<Rol, Usuario> store, ICacheManager cacheManager,
            IUnitOfWorkManager unitOfWorkManager) : base(store, cacheManager, unitOfWorkManager)
        {
        }
    }


    /// <summary>
    /// Extends <see cref="RoleManager{TRole,TKey}"/> of ASP.NET Identity Framework.
    /// Applications should derive this class with appropriate generic arguments.
    /// </summary>
    public abstract class AspRoleManager<TRole, TUser>
        : RoleManager<TRole, int>,
        IDomainService
        where TRole : AspRole<TUser>, new()
        where TUser : AspUser<TUser>
    {
        private readonly ICacheManager cacheManager;
        private readonly IUnitOfWorkManager unitOfWorkManager;

        protected AspRoleStore<TRole, TUser> AspStore { get; private set; }

        public AspRoleManager(
             AspRoleStore<TRole, TUser> store,
            ICacheManager cacheManager,
            IUnitOfWorkManager unitOfWorkManager) : base(store)
        {
            this.cacheManager = cacheManager;
            this.unitOfWorkManager = unitOfWorkManager;
            this.AspStore = store;
        }

        /// <summary>
        /// Creates a role.
        /// </summary>
        /// <param name="role">Role</param>
        public override async Task<IdentityResult> CreateAsync(TRole role)
        {
            var result = await CheckDuplicateRoleNameAsync(role.Id, role.Name);
            if (!result.Succeeded)
            {
                return result;
            }
            return await base.CreateAsync(role);
        }

        public override async Task<IdentityResult> UpdateAsync(TRole role)
        {
            var result = await CheckDuplicateRoleNameAsync(role.Id, role.Name);
            if (!result.Succeeded)
            {
                return result;
            }

            return await base.UpdateAsync(role);
        }

        /// <summary>
        /// Deletes a role.
        /// </summary>
        /// <param name="role">Role</param>
        public async override Task<IdentityResult> DeleteAsync(TRole role)
        {
    
            return await base.DeleteAsync(role);
        }

        /// <summary>
        /// Gets a role by given id.
        /// Throws exception if no role with given id.
        /// </summary>
        /// <param name="roleId">Role id</param>
        /// <returns>Role</returns>
        /// <exception cref="AbpException">Throws exception if no role with given id</exception>
        public virtual async Task<TRole> GetRoleByIdAsync(int roleId)
        {
            var role = await FindByIdAsync(roleId);
            if (role == null)
            {
                throw new Exception("There is no role with id: " + roleId);
            }

            return role;
        }

        /// <summary>
        /// Gets a role by given name.
        /// Throws exception if no role with given roleName.
        /// </summary>
        /// <param name="roleName">Role name</param>
        /// <returns>Role</returns>
        /// <exception cref="AbpException">Throws exception if no role with given roleName</exception>
        public virtual async Task<TRole> GetRoleByNameAsync(string roleName)
        {
            var role = await FindByNameAsync(roleName);
            if (role == null)
            {
                throw new Exception("There is no role with name: " + roleName);
            }

            return role;
        }

        public virtual async Task<IdentityResult> CheckDuplicateRoleNameAsync(int? expectedRoleId, string name)
        {
            var role = await FindByNameAsync(name);
            if (role != null && role.Id != expectedRoleId)
            {
                return IdentityResult.Failed(string.Format("El {0}, ya existe. No se puede duplicar los roles", name));
            }

            return IdentityResult.Success;
        }

       
    }
        

}
