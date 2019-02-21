using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace com.cpp.calypso.comun.dominio
{
    public class ActiveDirectoryPasswordManager : IPasswordManager<Usuario>
    {
        #region Variables

        private string sDomain;
        private string sDefaultOU;
        private string sDefaultRootOU;

        private string sServiceUser;
        private string sServicePassword;


        #endregion


        public ActiveDirectoryPasswordManager(
            string domain, 
            string connectionUsername,
            string connectionPassword)
        {

            this.sDomain = domain;
            this.sServiceUser = connectionUsername;
            this.sServicePassword = connectionPassword;
        }


        public ActiveDirectoryPasswordManager(
            string domain,
            string connectionString,
            string connectionUsername,
            string connectionPassword)
        {

            this.sDomain = domain;
            this.sDefaultOU = connectionString;
            this.sServiceUser = connectionUsername;
            this.sServicePassword = connectionPassword;

        }

        public async Task<PasswordVerificationResult> ValidateCredentials(Usuario user, string plainPassword)
        {
            return await Task.Run(() =>
            {
                var result = ValidateCredentials(user.UserName, plainPassword);
                if (result)
                    return PasswordVerificationResult.Success;

                return PasswordVerificationResult.Failed;
            });
 
           
        }

        /// <summary>
        /// Validates the username and password of a given user
        /// </summary>
        /// <param name="sUserName">The username to validate</param>
        /// <param name="sPassword">The password of the username to validate</param>
        /// <returns>Returns True of user is valid</returns>
        private bool ValidateCredentials(string sUserName, string sPassword)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();
            return oPrincipalContext.ValidateCredentials(sUserName, sPassword);
        }


        #region Helper Methods

        /// <summary>
        /// Gets the base principal context
        /// </summary>
        /// <returns>Returns the PrincipalContext object</returns>
        public PrincipalContext GetPrincipalContext()
        {
            if (string.IsNullOrWhiteSpace(sDefaultOU))
            {

                return new PrincipalContext
                     (ContextType.Domain, sDomain,
                     sServiceUser, sServicePassword);
            }
            else
            {
                PrincipalContext oPrincipalContext = new PrincipalContext
               (ContextType.Domain, sDomain, sDefaultOU,
               ContextOptions.SimpleBind,
               sServiceUser, sServicePassword);

                return oPrincipalContext;

            }
        }

        /// <summary>
        /// Gets the principal context on specified OU
        /// </summary>
        /// <param name="sOU">The OU you want your Principal Context to run on</param>
        /// <returns>Returns the PrincipalContext object</returns>
        public PrincipalContext GetPrincipalContext(string sOU)
        {
            PrincipalContext oPrincipalContext =
               new PrincipalContext(ContextType.Domain, sDomain, sOU,
               ContextOptions.SimpleBind, sServiceUser, sServicePassword);
            return oPrincipalContext;
        }

   

        #endregion
    }

}