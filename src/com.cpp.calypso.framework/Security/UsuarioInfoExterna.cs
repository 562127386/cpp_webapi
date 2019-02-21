using System.Collections.Generic;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Información Externa de un usuario
    /// </summary>
    public class ExternalInfoUser
    {
        /// <summary>
        /// Nombre del Usuario
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Identificacion
        /// </summary>
        public string Identificacion { get; set; }


        public string Nombres { get; set; }

        public string AperllidoPaterno { get; set; }

        /// <summary>
        /// Listado de Roles que posee el usuario 
        /// </summary>
        public List<string> RolesAD { get; set; }

     
        /// <summary>
        /// Correo electronico 
        /// </summary>
        public string Correo { get; set; }
        

        public ExternalInfoUser() {
            RolesAD = new List<string>();
        }
        
    }
}
