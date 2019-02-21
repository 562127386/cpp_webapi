using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.comun.dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace com.cpp.calypso.seguridad.aplicacion
{
    /// <summary>
    /// Representa un Usuario
    /// </summary>
    [Serializable]
    [DisplayName("Usuarios")]
    [AutoMap(typeof(Usuario))]
    public class UsuarioDto : EntityDto
    {
        public UsuarioDto()
        {
            Roles = new List<RolDto>();
        }

       
        [Obligado]
        [LongitudMayor(60)]
        public string Cuenta { get; set; }

        [Obligado]
        [LongitudMayor(80)]
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; }


        [Obligado]
        [LongitudMayor(80)]
        public string Nombres { get; set; }


        [Obligado]
        [LongitudMayor(80)]
        public string Apellidos { get; set; }

        [Obligado]
        [LongitudMayor(80)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Correo { get; set; }

      

        [Obligado]
        public EstadoUsuario Estado { get; set; }

        /// <summary>
        /// Listado de Roles que posee el Usuario
        /// </summary>
        public virtual ICollection<RolDto> Roles { get; set; }

        /// <summary>
        /// Listado de Modulos que posee el Usuario
        /// </summary>
        public virtual ICollection<ModuloDto> Modulos { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} ", Nombres, Apellidos);
        }

        public string NombresCompletos()
        {
            return string.Format("{0} {1} ", Nombres, Apellidos);
        }
    }
}
