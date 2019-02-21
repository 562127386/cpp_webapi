using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace com.cpp.calypso.comun.dominio
{
    public class ByPassPasswordManager : IPasswordManager<Usuario>
    {
        public async Task<PasswordVerificationResult> ValidateCredentials(Usuario user, string plainPassword)
        {
            return await Task.Run(() =>
            {
                    return PasswordVerificationResult.Success; 
            });
        }
    }
}