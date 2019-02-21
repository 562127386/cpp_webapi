using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace com.cpp.calypso.comun.dominio
{
    public interface IPasswordManager<TUser> where
        TUser : AspUser<TUser>
    {
        Task<PasswordVerificationResult> ValidateCredentials(TUser user,
            string plainPassword);
    }
}