using System;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Web.Configuration;
using System.Web.Security;
using System.Diagnostics;
using CommonServiceLocator;
using System.Configuration.Provider;
using System.Configuration;
using System.DirectoryServices;

namespace com.cpp.calypso.framework
{
    internal enum DirectoryType
    {
        AD = 0,
        ADAM = 1,
        Unknown = 2
    }

    /// <summary>
    /// Implementación para saltar la auttentificacion con clave sobre el directorio activo, unicamente se verificar si la identidad existe
    /// </summary>
    public class ByPassActiveDirectoryMembershipProvider : MembershipProvider
    {
 
        //Logger de la aplicacion
        private static readonly ILogger Log =
            ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(ByPassActiveDirectoryMembershipProvider));


        //
        // keeps track of whether the provider has already been initialized
        //
        private bool initialized = false;

        //
        // configuration parameters common to all membership providers
        //


        private string adConnectionString { get; set; }
        private string ConnectionUsername { get; set; }
        private string ConnectionPassword { get; set; }


    

        //
        // user account flags
        //
        private const int UF_ACCOUNT_DISABLED = 0x2;
        private const int UF_LOCKOUT = 0x10;
        private readonly DateTime DefaultLastLockoutDate = new DateTime(1754, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private const int AD_SALT_SIZE_IN_BYTES = 16;

        //
        // custom schema mappings (and their default values)
        //
        private string AttributeMapUsername = "userPrincipalName";
        private string AttributeMapEmail = "mail";




        private string AttributeMapPasswordQuestion = null;
        private string AttributeMapPasswordAnswer = null;
        private string AttributeMapFailedPasswordAnswerCount = null;
        private string AttributeMapFailedPasswordAnswerTime = null;
        private string AttributeMapFailedPasswordAnswerLockoutTime = null;


        private int passwordAnswerAttemptLockoutDuration;


        private bool enablePasswordReset;


        //
        // configuration parameters specific to the AD membership provider
        // and related to the directory connection are stored within the DirectoryInformation class
        //
        //DirectoryInformation directoryInfo = null;


        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            Log.Debug("Inicializando una instancia de ByPassActiveDirectoryMembershipProvider");

            //adConnectionString = config["connectionStringName"];
            ConnectionUsername = config["connectionUsername"];
            ConnectionPassword = config["connectionPassword"];
            AttributeMapUsername = config["attributeMapUsername"];



            if (config["applicationName"] == null || config["applicationName"].Trim() == "")
            {
                pApplicationName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
            }
            else
            {
                pApplicationName = config["applicationName"];
            }



            base.Initialize(name, config);

            string temp = config["connectionStringName"];
            if (String.IsNullOrEmpty(temp))
                throw new ProviderException("No se ha especificado la configuración de connectionStringName. Cadena de conexión al directorio activo");
 

            adConnectionString = GetConnectionString(temp, true);
            if (String.IsNullOrEmpty(adConnectionString))
                throw new ProviderException(string.Format("No se encuentra la configuración con la clave {0}",temp));


            passwordAnswerAttemptLockoutDuration = SecurityUtility.GetIntValue(config, "passwordAnswerAttemptLockoutDuration", 30, false, 0);

            enablePasswordReset = SecurityUtility.GetBooleanValue(config, "enablePasswordReset", false);


            //
            //  This will make some checks regarding whether the connectionProtection is valid (choose the right
            //  connectionprotection if necessary, make sure credentials are valid, container exists and the directory is
            //  either AD or ADAM)
            //
            //directoryInfo = new DirectoryInformation(adConnectionString, credential, connProtection, clientSearchTimeout, serverSearchTimeout, enablePasswordReset);


            initialized = true;
        }

        private string GetConnectionString(string connectionStringName, bool appLevel)
        {
            if (String.IsNullOrEmpty(connectionStringName))
                return null;

             
            //RuntimeConfig config = (appLevel) ? RuntimeConfig.GetAppConfig() : RuntimeConfig.GetConfig();
            //ConnectionStringSettings connObj = config.ConnectionStrings.ConnectionStrings[connectionStringName];

            ConnectionStringSettings connObj =  WebConfigurationManager.ConnectionStrings[connectionStringName]; //.ConnectionString;

            if (connObj == null)
            {
                //
                // No connection string by the specified name
                //
                throw new ProviderException(string.Format("No se encuentra la configuración con la clave {0}",connectionStringName));
            }

            return connObj.ConnectionString;
        }



        public override bool ValidateUser(string username, string password)
        {

            var root = new DirectoryEntry(adConnectionString, ConnectionUsername, ConnectionPassword);

            var searcher = new DirectorySearcher(root, string.Format(CultureInfo.InvariantCulture, "(&(objectClass=user)({0}={1}))", AttributeMapUsername, username));
           
             SearchResult result = searcher.FindOne();

             if (result != null && !string.IsNullOrEmpty(result.Path))
             {
                 DirectoryEntry user = result.GetDirectoryEntry();
                 
                 return true;
              
             }

             return false;
        }


        private string pApplicationName;


        public override string ApplicationName
        {
            get { return pApplicationName; }
            set { pApplicationName = value; }
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {

            //if (!initialized)
            //    throw new InvalidOperationException(SR.GetString(SR.ADMembership_Provider_not_initialized));

            //CheckUserName(ref username, maxUsernameLength, "username");

            Guard.AgainstNullOrEmptyString(username, "username");

            var root = new DirectoryEntry(adConnectionString, ConnectionUsername, ConnectionPassword);

            var searcher = new DirectorySearcher(root, string.Format(CultureInfo.InvariantCulture, "(&(objectClass=user)({0}={1}))", AttributeMapUsername, username));


            //
            // load all the attributes needed to create a MembershipUser object
            //
            searcher.PropertiesToLoad.Add(AttributeMapUsername);
            searcher.PropertiesToLoad.Add("objectSid");
            searcher.PropertiesToLoad.Add(AttributeMapEmail);
            searcher.PropertiesToLoad.Add("comment");
            searcher.PropertiesToLoad.Add("whenCreated");
            searcher.PropertiesToLoad.Add("pwdLastSet");
            searcher.PropertiesToLoad.Add("msDS-User-Account-Control-Computed");
            searcher.PropertiesToLoad.Add("lockoutTime");

            //if (retrieveSAMAccountName)
            //    searcher.PropertiesToLoad.Add("sAMAccountName");

            if (AttributeMapPasswordQuestion != null)
                searcher.PropertiesToLoad.Add(AttributeMapPasswordQuestion);

            //if (directoryInfo.DirectoryType == DirectoryType.AD)
                searcher.PropertiesToLoad.Add("userAccountControl");
            //else
            //    searcher.PropertiesToLoad.Add("msDS-UserAccountDisabled");

            if (EnablePasswordReset)
            {
                searcher.PropertiesToLoad.Add(AttributeMapFailedPasswordAnswerCount);
                searcher.PropertiesToLoad.Add(AttributeMapFailedPasswordAnswerTime);
                searcher.PropertiesToLoad.Add(AttributeMapFailedPasswordAnswerLockoutTime);
            }


            //DirectoryEntry entry3 = new DirectoryEntry(adConnectionString, ConnectionUsername, ConnectionPassword, this.AuthenticationTypes);
            //NativeComInterfaces.IAdsLargeInteger propertyValue = (NativeComInterfaces.IAdsLargeInteger)entry3.Properties["lockoutDuration"].Value;
            //long num2 = (propertyValue.HighPart * 0x100000000L) + ((uint)propertyValue.LowPart);
            //this.adLockoutDuration = new TimeSpan(-num2);


            SearchResult result = searcher.FindOne();

            if (result != null && !string.IsNullOrEmpty(result.Path))
            {
                return GetMembershipUserFromSearchResult(result);

            }

            return null;
        }



        private MembershipUser GetMembershipUserFromSearchResult(SearchResult res)
        {
            // username
            string username = (string)PropertyManager.GetSearchResultPropertyValue(res, AttributeMapUsername);

            // providerUserKey is the SID of the user
            byte[] sidBinaryForm = (byte[])PropertyManager.GetSearchResultPropertyValue(res, "objectSid");
            object providerUserKey = new SecurityIdentifier(sidBinaryForm, 0);

            // email (optional)
            string email = (res.Properties.Contains(AttributeMapEmail)) ? (string)res.Properties[AttributeMapEmail][0] : null;

            // passwordQuestion
            string passwordQuestion = null;
            if ((AttributeMapPasswordQuestion != null) && (res.Properties.Contains(AttributeMapPasswordQuestion)))
                passwordQuestion = (string)PropertyManager.GetSearchResultPropertyValue(res, AttributeMapPasswordQuestion);

            //comment (optional)
            string comment = (res.Properties.Contains("comment")) ? (string)res.Properties["comment"][0] : null;

            //isApproved and isLockedOut
            bool isApproved;
            bool isLockedOut = false;
            //if (directoryInfo.DirectoryType == DirectoryType.AD)
            //{
                int val = (int)PropertyManager.GetSearchResultPropertyValue(res, "userAccountControl");
                if ((val & UF_ACCOUNT_DISABLED) == 0)
                    isApproved = true;
                else
                    isApproved = false;

                //
                // the "msDS-User-Account-Control-Computed" is the correct attribute to determine if  the
                // user is locked out or not. This attribute does not exist in W2K schema, so if we do not see this attribute in the result set
                // we will use the "lockoutTime". Note, if the user is not locked out and the schema is W2K3, this attribute will exist in the result
                // and have value 0 (since it's constructed), therefore absence of the attribute signifies that schema is W2K.
                //
                if (res.Properties.Contains("msDS-User-Account-Control-Computed"))
                {
                    int val2 = (int)PropertyManager.GetSearchResultPropertyValue(res, "msDS-User-Account-Control-Computed");
                    if ((val2 & UF_LOCKOUT) != 0)
                        isLockedOut = true;
                }
                else if (res.Properties.Contains("lockoutTime"))
                {
                    // NOTE: all date-time computation is done in UTC time though the values returned are in local time
                    DateTime lockoutTime = DateTime.FromFileTimeUtc((Int64)PropertyManager.GetSearchResultPropertyValue(res, "lockoutTime"));
                    DateTime currentTime = DateTime.UtcNow;
                    TimeSpan diffTime = currentTime.Subtract(lockoutTime);

                    //TODO: JSA. No utilizar la forma correcta, ya que no se puede recuperar la informacion de DirectoryInformation, por ser private. 
                    //isLockedOut = (diffTime <= directoryInfo.ADLockoutDuration);

                    //TODO: JSA, siempre regresar falso
                    isLockedOut = false;
                }

            //}
            //else
            //{
            //    isApproved = true; // if the msDS-UserAccountDisabled attribute if not present then the user is enabled

            //    if (res.Properties.Contains("msDS-UserAccountDisabled"))
            //        isApproved = !((bool)PropertyManager.GetSearchResultPropertyValue(res, "msDS-UserAccountDisabled"));

            //    //
            //    // ADAM schema contains the "msDS-User-Account-Control-Computed" attribute, therefore it is used to determine the
            //    // lockout status of the user
            //    //
            //    int val2 = (int)PropertyManager.GetSearchResultPropertyValue(res, "msDS-User-Account-Control-Computed");
            //    if ((val2 & UF_LOCKOUT) != 0)
            //        isLockedOut = true;
            //}

            // lastLockoutDate (DateTime.FromFileTime cnoverts to Local time)
            DateTime lastLockoutDate = DefaultLastLockoutDate;
            if (isLockedOut)
                lastLockoutDate = DateTime.FromFileTime((Int64)PropertyManager.GetSearchResultPropertyValue(res, "lockoutTime"));

            //
            // if password reset is enabled, we need to check if user is locked out due to bad password answer (and set/change the last lockout date)
            //
            if ((EnablePasswordReset) && (res.Properties.Contains(AttributeMapFailedPasswordAnswerLockoutTime)))
            {
                // NOTE: all date-time computation is done in UTC time though the values returned are in local time
                DateTime badPasswordAnswerLockoutTime = DateTime.FromFileTimeUtc((Int64)PropertyManager.GetSearchResultPropertyValue(res, AttributeMapFailedPasswordAnswerLockoutTime));
                DateTime currentTime = DateTime.UtcNow;
                TimeSpan diffTime = currentTime.Subtract(badPasswordAnswerLockoutTime);
                bool isLockedOutByBadPasswordAnswer = (diffTime <= new TimeSpan(0, passwordAnswerAttemptLockoutDuration, 0));

                if (isLockedOutByBadPasswordAnswer)
                {
                    if (isLockedOut)
                    {
                        //
                        // The account is locked both due to bad password and bad password answer, so we have two lockout dates
                        // Taking the later one.
                        //
                        if (DateTime.Compare(badPasswordAnswerLockoutTime, DateTime.FromFileTimeUtc((Int64)PropertyManager.GetSearchResultPropertyValue(res, "lockoutTime"))) > 0)
                            lastLockoutDate = DateTime.FromFileTime((Int64)PropertyManager.GetSearchResultPropertyValue(res, AttributeMapFailedPasswordAnswerLockoutTime));
                    }
                    else
                    {
                        //
                        // Account is locked out only due to bad password answer
                        //
                        isLockedOut = true;
                        lastLockoutDate = DateTime.FromFileTime((Int64)PropertyManager.GetSearchResultPropertyValue(res, AttributeMapFailedPasswordAnswerLockoutTime));
                    }
                }
            }

            //createTimeStamp
            DateTime whenCreated = ((DateTime)PropertyManager.GetSearchResultPropertyValue(res, "whenCreated")).ToLocalTime();

            //lastLogon (not supported)
            DateTime lastLogon = DateTime.MinValue;

            //lastActivity (not supported)
            DateTime lastActivity = DateTime.MinValue;

            //lastpwdchange (DateTime.FromFileTime cnoverts to Local time)
            DateTime lastPasswordChange = DateTime.FromFileTime((Int64)PropertyManager.GetSearchResultPropertyValue(res, "pwdLastSet"));

            return new ActiveDirectoryMembershipUser(Name, username, providerUserKey, email, passwordQuestion, comment, isApproved, isLockedOut, whenCreated, lastLogon, lastActivity, lastPasswordChange, lastLockoutDate); //, true /* valuesAreUpdated */);
        }


        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get
            {
                if (!initialized)
                   throw new InvalidOperationException(string.Format("Proveedor no ha sido inicializado "));
 
                return enablePasswordReset;
            }
        
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

      



        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        
    }

    internal static class PropertyManager
    {
        public static object GetPropertyValue(DirectoryEntry directoryEntry, string propertyName)
        {

            Debug.Assert(directoryEntry != null, "PropertyManager::GetPropertyValue - directoryEntry is null");
            Debug.Assert(propertyName != null, "PropertyManager::GetPropertyValue - propertyName is null");

            if (directoryEntry.Properties[propertyName].Count == 0)
            {
                if (directoryEntry.Properties["distinguishedName"].Count != 0)
                    throw new ProviderException(string.Format("La propiedad {0} no se encuentra", propertyName, (string)directoryEntry.Properties["distinguishedName"].Value));
                else
                    throw new ProviderException(string.Format("La propiedad {0} no se encuentra", propertyName));
            }

            return directoryEntry.Properties[propertyName].Value;
        }

        public static object GetSearchResultPropertyValue(SearchResult res, string propertyName)
        {

            Debug.Assert(res != null, "PropertyManager::GetSearchResultPropertyValue - res is null");
            Debug.Assert(propertyName != null, "PropertyManager::GetSearchResultPropertyValue - propertyName is null");

            ResultPropertyValueCollection propertyValues = null;

            propertyValues = res.Properties[propertyName];
            if ((propertyValues == null) || (propertyValues.Count < 1))
                throw new ProviderException(string.Format("La propiedad {0} no se encuentra", propertyName));

            return propertyValues[0];
        }
    }
}
