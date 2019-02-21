using Abp.Dependency;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace com.cpp.calypso.comun.dominio
{
    public class RoleStore : AspRoleStore<Rol, Usuario>
    {
        public RoleStore(IBaseRepository<Rol> roleRepository) : base(roleRepository)
        {
        }
    }

    /// <summary>
    /// Implements 'Role Store' of ASP.NET Identity Framework.
    /// </summary>
    public abstract class AspRoleStore<TRole, TUser> :
        IQueryableRoleStore<TRole, int>,
        ITransientDependency
        where TRole : AspRole<TUser>
        where TUser : AspUser<TUser>
    {
        private readonly IBaseRepository<TRole> _roleRepository;
       // private readonly IBaseRepository<UserRole, long> _userRoleRepository;
    
        /// <summary>
        /// Constructor.
        /// </summary>
        protected AspRoleStore(
            IBaseRepository<TRole> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public virtual IQueryable<TRole> Roles
        {
            get { return _roleRepository.GetAll(); }
        }

        public virtual async Task CreateAsync(TRole role)
        {
            await _roleRepository.InsertAsync(role);
        }

        public virtual async Task UpdateAsync(TRole role)
        {
            await _roleRepository.UpdateAsync(role);
        }

        public virtual async Task DeleteAsync(TRole role)
        {
            //await _userRoleRepository.DeleteAsync(ur => ur.RoleId == role.Id);

            await _roleRepository.DeleteAsync(role);
        }

        public virtual async Task<TRole> FindByIdAsync(int roleId)
        {
            return await _roleRepository.FirstOrDefaultAsync(roleId);
        }

        public virtual async Task<TRole> FindByNameAsync(string roleName)
        {
            return await _roleRepository.FirstOrDefaultAsync(
                role => role.Nombre == roleName
                );
        }

       
        public virtual void Dispose()
        {
            //No need to dispose since using IOC.
        }
    }
        

}
