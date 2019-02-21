using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Globalization;
using System.Linq;
using System.Web.Configuration;

namespace com.cpp.calypso.framework
{

    /// <summary>
    /// Proveedor para obtener información externa desde el directorio activo
    /// </summary>
    public class ActiveDirectoryExternalInfoUserProvider :  IExternalInfoUserProvider
    {

        //Logger de la aplicacion
        private static readonly ILogger Log =
            ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(ActiveDirectoryExternalInfoUserProvider));


        private string ConnectionStringName { get; set; }
        private string ConnectionUsername { get; set; }
        private string ConnectionPassword { get; set; }

        
        private string AttributeMapRoles { get; set; }
        
        private string AttributeMapUsername { get; set; }

        private string AttributeMapUserPrincipalName { get; set; }

        private string AttributeMapBannerId { get; set; }
        
        private string AttributeMapNumeroMatricula { get; set; }
        
        private string AttributeMapApellidoPaterno { get; set; }

        private string AttributeMapNombres { get; set; }

        private string AttributeMapIdentificacion { get; set; }

        private string AttributeMapCorreo { get; set; }

        private string AttributeMapOtroCorreo { get; set; }

        private string AttributeMapPersonaCorreo { get; set; }

        private string AttributeMapSeparadorCorreos { get; set; }

        private string AttributeMapSatatusEstudiante { get; set; }

        private string AttributeMapStatusProfesor { get; set; }


        public ActiveDirectoryExternalInfoUserProvider(
            string connectionStringName,
            string connectionUsername,     
            string connectionPassword,
            string attributeMapUsername,
            string attributeMapRoles,
            string attributeMapUserPrincipalName,
            string attributeMapBannerId,
            string attributeMapNumeroMatricula,
            string attributeMapApellidoPaterno,
            string attributeMapNombres,
            string attributeMapIdentificacion,
            string attributeMapCorreo,
            string attributeMapOtroCorreo,
            string attributeMapPersonaCorreo,
            string attributeMapSeparadorCorreos,
            string attributeMapSatatusEstudiante,
            string attributeMapStatusProfesor)
        {
            ConnectionStringName = connectionStringName;
            ConnectionUsername = connectionUsername;
            ConnectionPassword = connectionPassword;   
 

            //Listado de Atributos para Mapeo contra el Active Directory

            AttributeMapUsername = attributeMapUsername;
            AttributeMapRoles = attributeMapRoles;
            AttributeMapUserPrincipalName = attributeMapUserPrincipalName;
            AttributeMapBannerId = attributeMapBannerId;
            AttributeMapNumeroMatricula = attributeMapNumeroMatricula;
            AttributeMapApellidoPaterno = attributeMapApellidoPaterno;
            AttributeMapNombres = attributeMapNombres;
            AttributeMapIdentificacion = attributeMapIdentificacion;
            AttributeMapCorreo = attributeMapCorreo;
            AttributeMapOtroCorreo = attributeMapOtroCorreo;
            AttributeMapPersonaCorreo = attributeMapPersonaCorreo;
            AttributeMapSeparadorCorreos = attributeMapSeparadorCorreos;
            AttributeMapSatatusEstudiante = attributeMapSatatusEstudiante;
            AttributeMapStatusProfesor = attributeMapStatusProfesor;
        }


 
        public ExternalInfoUser GetAtributosForUser(string username)
        {
            Log.DebugFormat("Recuperar informacion del username {0} desde ActiveDirectory", username);


            var usuarioInfoExterna = new ExternalInfoUser();

            var root = new DirectoryEntry(WebConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString, ConnectionUsername, ConnectionPassword);
            var searcher = new DirectorySearcher(root, string.Format(CultureInfo.InvariantCulture, "(&(objectClass=user)({0}={1}))", AttributeMapUsername, username));

            //
            // load all the attributes needed to create a external info user
            //
            searcher.PropertiesToLoad.Add(AttributeMapRoles);
            searcher.PropertiesToLoad.Add(AttributeMapUsername);
            searcher.PropertiesToLoad.Add(AttributeMapUserPrincipalName);
            searcher.PropertiesToLoad.Add(AttributeMapBannerId);
            searcher.PropertiesToLoad.Add(AttributeMapNumeroMatricula);
            searcher.PropertiesToLoad.Add(AttributeMapApellidoPaterno);
            searcher.PropertiesToLoad.Add(AttributeMapNombres);
            searcher.PropertiesToLoad.Add(AttributeMapIdentificacion);
            searcher.PropertiesToLoad.Add(AttributeMapCorreo);
            searcher.PropertiesToLoad.Add(AttributeMapOtroCorreo);
            searcher.PropertiesToLoad.Add(AttributeMapPersonaCorreo);
            searcher.PropertiesToLoad.Add(AttributeMapSatatusEstudiante);
            searcher.PropertiesToLoad.Add(AttributeMapStatusProfesor);

           

            SearchResult result = searcher.FindOne();

            if (result != null && !string.IsNullOrEmpty(result.Path))
            {
                DirectoryEntry user = result.GetDirectoryEntry();

        
                PropertyValueCollection groupsRoles = user.Properties[AttributeMapRoles];
                PropertyValueCollection userName = user.Properties[AttributeMapUsername];
                PropertyValueCollection userPrincipalName = user.Properties[AttributeMapUserPrincipalName];
                PropertyValueCollection bannerId = user.Properties[AttributeMapBannerId];
                PropertyValueCollection matriculaId = user.Properties[AttributeMapNumeroMatricula];
                PropertyValueCollection aperllidoPaterno = user.Properties[AttributeMapApellidoPaterno];
                PropertyValueCollection nombres = user.Properties[AttributeMapNombres];
                PropertyValueCollection identificacion = user.Properties[AttributeMapIdentificacion];
                PropertyValueCollection correo = user.Properties[AttributeMapCorreo];
                PropertyValueCollection otroCorreo = user.Properties[AttributeMapOtroCorreo];
                PropertyValueCollection personaCorreo = user.Properties[AttributeMapPersonaCorreo];
                PropertyValueCollection statusEstudiante = user.Properties[AttributeMapSatatusEstudiante];
                PropertyValueCollection statusProfesor = user.Properties[AttributeMapStatusProfesor];



                foreach (string path in groupsRoles)
                {
                    string[] parts = path.Split(',');

                    if (parts.Length > 0)
                    {
                        foreach (string part in parts)
                        {
                            string[] p = part.Split('=');

                            if (p[0].Equals("cn", StringComparison.OrdinalIgnoreCase))
                            {
                                usuarioInfoExterna.RolesAD.Add(p[1]);
                            }

                        }
                    }

                }
                //allRoles.Add(bannerId.Value.ToString());
                //allRoles.Add(matriculaId.Value.ToString());

                if (userName.Value != null)
                {
                    usuarioInfoExterna.UserName = userName.Value.ToString();
                }

                //if (userPrincipalName.Value == null)
                //{

                //    UserPrincipalName = "";
                //}
                //else
                //{
                //    UserPrincipalName = userPrincipalName.Value.ToString();
                //}

                if (aperllidoPaterno.Value != null)
                {
                    usuarioInfoExterna.AperllidoPaterno = aperllidoPaterno.Value.ToString();
                }

                if (nombres.Value != null)
                {
                    usuarioInfoExterna.Nombres = nombres.Value.ToString();
                }

                if (identificacion.Value != null)
                {
                    usuarioInfoExterna.Identificacion = identificacion.Value.ToString();
                }

                List<string> correoList=new List<string>();

                if (correo.Value != null)
                {
                    correoList.Add(correo.Value.ToString());
                    
                }
                if (otroCorreo.Value != null)
                {
                    correoList.Add(otroCorreo.Value.ToString());
                    
                }
                if (personaCorreo.Value != null)
                {
                    correoList.Add(personaCorreo.Value.ToString());
                    
                }
                if (correoList.Count > 0)
                {
                    var correosNoRepetidos = correoList.Distinct();
                    usuarioInfoExterna.Correo = correosNoRepetidos.Aggregate((current, next) => current + AttributeMapSeparadorCorreos + next);
                }

                //if (statusEstudiante.Value == null)
                //{

                //    StatusEstudiante = "";
                //}
                //else
                //{
                //    StatusEstudiante = statusEstudiante.Value.ToString();
                //}

                //if (statusProfesor.Value == null)
                //{

                //    StatusProfesor = "";
                //}
                //else
                //{
                //    StatusProfesor = statusProfesor.Value.ToString();
                //}

                return usuarioInfoExterna;

            }

            return null;
        }
         
    }

   
}
