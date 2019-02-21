using System.Security.Claims;

namespace com.cpp.calypso.comun.dominio
{
    public class LoginResult<TUser>
        where TUser : AspUser<TUser>
    {
        public Modulo Modulo { get; private set; } 

        public LoginResultType Result { get; private set; }
 
        public TUser User { get; private set; }

        public ClaimsIdentity Identity { get; private set; }

        public LoginResult(LoginResultType result,  TUser user = null, Modulo modulo = null)
        {
            Result = result;

            Modulo = modulo;
            User = user;
        }

        public LoginResult(Modulo modulo,TUser user, ClaimsIdentity identity)
           : this(LoginResultType.Success, user,  modulo)
        {
            Identity = identity;
        }

    }
}
