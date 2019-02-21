using System.Collections.Generic;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Informacion de un Usuario Autentificado
    /// </summary>
    public class UsuarioAutentificado
    {
        public UsuarioAutentificado()
        {
            Roles = new List<RolAutentificado>();
            Modulos = new List<ModuloAutentificado>();
        }

        public int Id { get; set; }

        public string Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Cuenta { get; set; }

        public virtual ICollection<RolAutentificado> Roles { get; set; }

        public virtual ICollection<ModuloAutentificado> Modulos { get; set; }
    }

}
