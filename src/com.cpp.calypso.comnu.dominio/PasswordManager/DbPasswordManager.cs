using Abp.Domain.Services;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace com.cpp.calypso.comun.dominio
{

    public class DbPasswordManager : DbAbstractPasswordManager<Usuario, Modulo,Rol>
    {
        public DbPasswordManager(AspUserManager<Rol, Usuario, Modulo> userManager) :
            base(userManager)
        {

        }
    }

    public abstract class DbAbstractPasswordManager<TUser, TModule, TRole> :
        IDomainService, IPasswordManager<TUser> 
         where TRole : AspRole<TUser>, new()
         where TModule : Modulo
         where TUser : AspUser<TUser>
    {
    

        private readonly AspUserManager<TRole, TUser, TModule> UserManager;

        public DbAbstractPasswordManager(
           AspUserManager<TRole, TUser, TModule> userManager)
        {
            this.UserManager = userManager;
        }

        public virtual async Task<PasswordVerificationResult> ValidateCredentials(TUser user, string plainPassword) {

            return await Task.Run(() =>
            {
                //3. Verificar Clave. (Realizar bloqueo por intentos fallidos)
                var verificationResult = UserManager.PasswordHasher.VerifyHashedPassword(user.UserName, plainPassword);

                return verificationResult;

            });
        }

     
    }
}
