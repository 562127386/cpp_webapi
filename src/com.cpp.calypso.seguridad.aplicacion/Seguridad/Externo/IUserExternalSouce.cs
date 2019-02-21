using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Globalization;

namespace com.cpp.calypso.seguridad.aplicacion
{
    public interface IUserExternalSouce
    {
        ExternalUser GetUser(string UserName);
    }

    public class ActiveDirectoryUserExternalSouce : IUserExternalSouce
    {

        private string sDomain;
        private string sDefaultOU;
        private string sDefaultRootOU;

        private string sServiceUser;
        private string sServicePassword;

 

        public ActiveDirectoryUserExternalSouce(
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

        public ExternalUser GetUser(string UserName)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();

            UserPrincipal oUserPrincipal =
               UserPrincipal.FindByIdentity(oPrincipalContext, UserName);

            ExternalUser externalUser = new ExternalUser();
            if (oUserPrincipal != null)
            {
                externalUser.UserName = oUserPrincipal.SamAccountName;
                externalUser.Surname = oUserPrincipal.Surname;
                externalUser.GivenName = oUserPrincipal.GivenName;
                externalUser.EmailAddress = oUserPrincipal.EmailAddress;
            }

            return externalUser;
        }

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
    }


    public class LdapUserExternalSouce : IUserExternalSouce
    {
        private AuthenticationTypes authenticationType;

        //1. Configuraciones
        private string ConnectionString;
        private string Domain;
        private string ConnectionUsername;
        private string ConnectionPassword;

        string AttributeMapUsername = "sAMAccountName";

        string FilterSearchUser = "(&(objectClass=user)({0}={1}))";

        //string AttributeMapUserPrincipalname = "userPrincipalName";


        public LdapUserExternalSouce(string domain,
            string connectionString,
            string connectionUsername,
            string connectionPassword)
        {

            this.Domain = domain;
            this.ConnectionString = connectionString;
            this.ConnectionUsername = connectionUsername;
            this.ConnectionPassword = connectionPassword;
            this.authenticationType = AuthenticationTypes.ServerBind;
        }




        public LdapUserExternalSouce(string domain,
         string connectionString,
         string connectionUsername,
         string connectionPassword,
         string attributeMapUsername)
        {

            this.Domain = domain;
            this.ConnectionString = connectionString;
            this.ConnectionUsername = connectionUsername;
            this.ConnectionPassword = connectionPassword;
            this.AttributeMapUsername = attributeMapUsername;
            this.authenticationType = AuthenticationTypes.ServerBind;
        }

        public LdapUserExternalSouce(string domain,
         string connectionString,
         string connectionUsername,
         string connectionPassword,
         string attributeMapUsername,
         string filterSearchUser)
        {

            this.Domain = domain;
            this.ConnectionString = connectionString;
            this.ConnectionUsername = connectionUsername;
            this.ConnectionPassword = connectionPassword;
            this.authenticationType = AuthenticationTypes.ServerBind;
            this.AttributeMapUsername = attributeMapUsername;
            this.FilterSearchUser = filterSearchUser;
        }

        public LdapUserExternalSouce(string domain,
        string connectionString,
        string connectionUsername,
        string connectionPassword,
        string attributeMapUsername,
        string filterSearchUser,
        int authenticationTypes)
        {

            this.Domain = domain;
            this.ConnectionString = connectionString;
            this.ConnectionUsername = connectionUsername;
            this.ConnectionPassword = connectionPassword;
            this.AttributeMapUsername = attributeMapUsername;
            this.FilterSearchUser = filterSearchUser;

            this.authenticationType = (AuthenticationTypes)authenticationTypes;
        }


        public LdapUserExternalSouce(string domain,
         string connectionString,
         string connectionUsername,
         string connectionPassword,
         string attributeMapUsername,
         AuthenticationTypes authenticationTypes)
        {

            this.Domain = domain;
            this.ConnectionString = connectionString;
            this.ConnectionUsername = connectionUsername;
            this.ConnectionPassword = connectionPassword;
            this.AttributeMapUsername = attributeMapUsername;
            this.authenticationType = authenticationTypes;
        }

        public LdapUserExternalSouce(string domain,
         string connectionString,
         string connectionUsername,
         string connectionPassword,
         string attributeMapUsername,
         int authenticationTypes)
        {

            this.Domain = domain;
            this.ConnectionString = connectionString;
            this.ConnectionUsername = connectionUsername;
            this.ConnectionPassword = connectionPassword;
            this.AttributeMapUsername = attributeMapUsername;
            this.authenticationType = (AuthenticationTypes)authenticationTypes;
        }

        public ExternalUser GetUser(string UserName)
        {
            var root = GetDirectoryEntry();
            DirectorySearcher search = new DirectorySearcher(root);
            search.Filter = string.Format(CultureInfo.InvariantCulture,
               FilterSearchUser, AttributeMapUsername, UserName);

            SearchResult result = search.FindOne();

            if (result != null && !string.IsNullOrEmpty(result.Path))
            {
                DirectoryEntry user = result.GetDirectoryEntry();

                //PrincipalContext principalContext = new PrincipalContext(
                //    ContextType.Domain, ConnectionString);

                ExternalUser externalUser = new ExternalUser();
                externalUser.UserName = UserName;

                PropertyValueCollection apellidoProperty = user.Properties["sn"];
                PropertyValueCollection nombresProperty = user.Properties["givenName"];
                PropertyValueCollection correoProperty = user.Properties["mail"];


                if (apellidoProperty.Value != null)
                {
                    externalUser.Surname = apellidoProperty.Value.ToString();
                }

                if (nombresProperty.Value != null)
                {
                    externalUser.GivenName = nombresProperty.Value.ToString();
                }

                if (correoProperty.Value != null)
                {
                    externalUser.EmailAddress = correoProperty.Value.ToString();
                }

                return externalUser;
            }
            else
            {
                return null;
            }
        }

        public DirectoryEntry GetDirectoryEntry()
        {

            ////
            //// Resumen:
            ////     Es igual a cero, lo que significa utilizar la autenticación básica (enlace simple)
            ////     en el proveedor LDAP.
            //None = 0,
            ////
            //// Resumen:
            ////     Solicita autenticación segura. Cuando se establece este indicador, el proveedor
            ////     de WinNT utiliza NTLM para autenticar al cliente. Los servicios de dominio de
            ////     Active Directory utiliza Kerberos y posiblemente NTLM, para autenticar al cliente.
            ////     Cuando el nombre de usuario y la contraseña son una referencia nula (Nothing
            ////     en Visual Basic), ADSI enlaza al objeto utilizando el contexto de seguridad del
            ////     subproceso que realiza la llamada, que es el contexto de seguridad de la cuenta
            ////     de usuario bajo la que se ejecuta la aplicación o de la cuenta de usuario de
            ////     cliente que el subproceso que realiza la llamada está suplantando.
            //Secure = 1,
            ////
            //// Resumen:
            ////     Asocia una firma criptográfica al mensaje que identifica al remitente y garantiza
            ////     que el mensaje no se ha modificado en tránsito.
            //Encryption = 2,
            ////
            //// Resumen:
            ////     Asocia una firma criptográfica al mensaje que identifica al remitente y garantiza
            ////     que el mensaje no se ha modificado en tránsito. Los servicios de dominio de Active
            ////     Directory requiere instalar el servidor de certificados para admitir el cifrado
            ////     de capa de Sockets seguros (SSL).
            //SecureSocketsLayer = 2,
            ////
            //// Resumen:
            ////     Para un proveedor de WinNT, ADSI intenta conectarse a un controlador de dominio.
            ////     Servicios de dominio de Active Directory, esta marca indica que un servidor de
            ////     escritura no se requiere para un enlace sin servidor.
            //ReadonlyServer = 4,
            ////
            //// Resumen:
            ////     No se realiza la autenticación.
            //Anonymous = 16,
            ////
            //// Resumen:
            ////     Especifica que ADSI no intentará consultar la propiedad objectClass de los servicios
            ////     de dominio de Active Directory. Por lo tanto, se expondrán sólo las interfaces
            ////     base que son compatibles con todos los objetos ADSI. Otras interfaces compatibles
            ////     con el objeto no estará disponibles. Un usuario puede utilizar esta opción para
            ////     mejorar el rendimiento de una serie de manipulaciones de objetos que sólo incluyan
            ////     los métodos de las interfaces base. Sin embargo, ADSI no comprueba si los objetos
            ////     de solicitud existen realmente en el servidor. Para obtener más información,
            ////     vea el tema "Fast Binding opción para lote Write/Modify Operations" en MSDN Library
            ////     en http://msdn.microsoft.com/library. Para obtener más información sobre la propiedad
            ////     objectClass, vea el tema "Object-Class" en MSDN Library en http://msdn.microsoft.com/library.
            //FastBind = 32,
            ////
            //// Resumen:
            ////     Comprueba la integridad de los datos para asegurarse de que los datos recibidos
            ////     son el mismo que los datos enviados. El System.DirectoryServices.AuthenticationTypes.Secure
            ////     indicador debe configurarse para utilizar la firma.
            //Signing = 64,
            ////
            //// Resumen:
            ////     Cifra los datos mediante Kerberos. El System.DirectoryServices.AuthenticationTypes.Secure
            ////     indicador también debe establecerse para que se utilice el sellado.
            //Sealing = 128,
            ////
            //// Resumen:
            ////     Permite que Active Directory Services Interface (ADSI) para delegar el contexto
            ////     de seguridad del usuario, que es necesario para mover objetos entre dominios.
            //Delegation = 256,
            ////
            //// Resumen:
            ////     Si el ADsPath correspondiente incluye un nombre de servidor, especifique este
            ////     indicador cuando utilice el proveedor LDAP. No utilice esta marca para rutas
            ////     de acceso que incluyan un nombre de dominio o de rutas de acceso sin servidor.
            ////     Especificar un nombre de servidor sin especificar este indicador da como resultado
            ////     el tráfico de red innecesario.
            //ServerBind = 512

            var root = new DirectoryEntry(
                ConnectionString,
                ConnectionUsername,
                ConnectionPassword,
                authenticationType);

            return root;
        }
    }

    public class ExternalUser
    {
        public string UserName { get; internal set; }
        public string Surname { get; internal set; }
        public string GivenName { get; internal set; }
        public string EmailAddress { get; internal set; }

        public override string ToString()
        {
            return string.Format("UserName: {0}, Surname: {1}, GivenName: {2}, EmailAddress: {3}",
                UserName, Surname, GivenName, EmailAddress
                    );
        }
    }
}

 