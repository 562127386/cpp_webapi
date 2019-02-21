using com.cpp.calypso.comun.dominio;
using CommonServiceLocator;
using System;
using System.DirectoryServices;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Implementacion de autentificacion basada en servicios LDAP
    /// </summary>
    public class LdapAuthentication : IAuthentication
    {
        /// <summary>
        /// Variable paraa realizar Log
        /// </summary>
        private static readonly ILogger Log =
            ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(LdapAuthentication));

        readonly string _domain;
        readonly string _path;
        private readonly bool _userNameCompareStrict;


        ///<summary>
        ///</summary>
        ///<param name="userNameCompareStrict"></param>
        public LdapAuthentication(string path, string domain, bool userNameCompareStrict)
        {
         
            Log.Debug("Se ha creado una instancia de LdapAuthentication");
            _path = string.Format("LDAP://{0}", path);
            _domain = domain;
            _userNameCompareStrict = userNameCompareStrict;
        }

        #region IAuthentication Members

        /// <summary>
        /// Autentica un usuario
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>

        /// <returns></returns>
        public bool Authenticate(string username, string password)
        {
            Guard.AgainstNullOrEmptyString(username, "username");
            Guard.AgainstNullOrEmptyString(username, "password");



            Log.DebugFormat("Autenticando el usuario {0}", username);
            var domainAndUsername = _domain + @"\" + username;
            //TODO: Definir un usuario administrador que pueda navegar por el arbol de directorios
            //var entry = new DirectoryEntry(_path, "egtorresx", "factory");
            try
            {
                var entry = new DirectoryEntry(_path, domainAndUsername, password);
                //  object obj = entry.NativeObject;
                var search = new DirectorySearcher(entry)
                {
                    Filter = "(SAMAccountName=" + username + ")"
                };

                search.PropertiesToLoad.Add("cn");
                //TODO: Agregar propiedad para obtener el id del usuario del Active Directory para sincronizar con el Syllabus
                search.PropertiesToLoad.Add("samaccountname");
                var result = search.FindOne();

                // Se agrega comprobación del UserName para distinguir entre mayúsculas y minúsculas
                var isEqualUserName = false;
                if (IsCompareStrictUserName())
                {
                    isEqualUserName = (String)result.Properties["samaccountname"][0] == username;
                }
                else
                {
                    isEqualUserName = ((String)result.Properties["samaccountname"][0]).ToLower() == username.ToLower();
                }

                if (null == result || !isEqualUserName)
                {
                    throw new LdapUsernameOrPasswordException();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new LdapUsernameOrPasswordException(ex);
            }

        }

        ///<summary>
        /// 
        ///</summary>
        ///<returns></returns>
        public bool IsCompareStrictUserName()
        {
            return _userNameCompareStrict;
        }

        #endregion
    }
}
