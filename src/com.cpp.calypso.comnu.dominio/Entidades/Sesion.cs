using System;
using System.ComponentModel;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Sesion de un usuario 
    /// </summary>
    [Serializable]
    [DisplayName("Sesiones de Usuario")]
    public class Sesion : Entity, IHasCreationTime, ISesion
    {
  
        /// <summary>
        /// Identificador del usuario
        /// </summary>
        public virtual int? UsuarioId { get; set; }

        /// <summary>
        /// cuenta del usuario
        /// </summary>
        [LongitudMayor(50)]
        public virtual string Cuenta { get; set; }

        /// <summary>
        /// Fecha
        /// </summary>
        public virtual DateTime CreationTime { get; set; }


        /// <summary>
        /// Direccion IP del usuario que inicio la sesion.  IP address of the client.
        /// </summary>
        [LongitudMayor(64)]
        public virtual string ClientIpAddress { get; set; }

        /// <summary>
        /// Name (generally computer name) of the client.
        /// </summary>
        [LongitudMayor(128)]
        public virtual string ClientName { get; set; }

        /// <summary>
        /// Browser information if this method is called in a web request.
        /// </summary>
        [LongitudMayor(512)]
        public virtual string BrowserInfo { get; set; }

      
        /// <summary>
        /// Identificador del Modulo con el cual inicio la sesion
        /// </summary>
        public int? ModuloId { get; set; }

        /// <summary>
        /// Login attempt result.
        /// </summary>
        public virtual LoginResultType Result { get; set; }


    }
}
