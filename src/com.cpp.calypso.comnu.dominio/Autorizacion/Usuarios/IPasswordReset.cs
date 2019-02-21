using System.Threading.Tasks;

namespace com.cpp.calypso.comun.dominio
{
    
    public interface IPasswordReset
    {
        string PasswordResetCode { get;   }

        void ClearPasswordResetCode(IPasswordReset user);

        void SetNewPasswordResetCode();

        Task<string> GetPasswordResetCode(IPasswordReset user);

    }
}