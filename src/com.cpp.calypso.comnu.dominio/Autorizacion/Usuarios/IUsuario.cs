using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.comun.dominio
{



    /// <summary>
    /// Interfaz de informacion de un usuario
    /// </summary>
    public interface IUsuario
    {
        int Id { get; set; }
        string Identificacion { get; set; }
        string Nombres { get; set; }
        string Apellidos { get; set; }
        string Correo { get; set; }
        string Cuenta { get; set; }
        
        
        string EstadoNombre { get; set; }
    }

}
