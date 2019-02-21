using System;
namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Interfaz para mantener informacion de una sesion de un usuario
    /// </summary>
    public interface ISesion
    {
        int? UsuarioId { get; set; }

        string Cuenta { get; set; }

        string BrowserInfo { get; set; }

        string ClientIpAddress { get; set; }

        string ClientName { get; set; }

        DateTime CreationTime { get; set; }
        
        int? ModuloId { get; set; }

        /// <summary>
        /// Resultado de Inicio de sesion del Usuario. 
        /// </summary>
        LoginResultType Result { get; set; }
    }

}
