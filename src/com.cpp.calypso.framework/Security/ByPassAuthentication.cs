using com.cpp.calypso.comun.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Implementación para saltar la auttentificacion
    /// </summary>
    public class ByPassAuthentication : IAuthentication
    {

        public bool Authenticate(string username, string password)
        {
            Guard.AgainstNullOrEmptyString(username, "username");
            Guard.AgainstNullOrEmptyString(username, "password");

            return true;
        }

        public bool IsCompareStrictUserName()
        {
            return true;
        }
    }

  
}
