using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.cpp.calypso.comun.dominio
{

    /// <summary>
    /// Representa un Usuario
    /// </summary>
    [Serializable]
    [DisplayName("Gestión de Usuarios")]
    public class Usuario : AspUser<Usuario>
    {
        [NotMapped]
        public string NombresCompletos
        {
            get => string.Format("{0} {1}", Nombres, Apellidos);
        }
        

        public Usuario()
        {
            //Default
            Estado = EstadoUsuario.Activo;

            Roles = new List<Rol>();
            Modulos = new List<Modulo>();

            
        }

    }

    public enum EstadoUsuario
    {
        Activo = 1,
        Inactivo = 0
    }
}
