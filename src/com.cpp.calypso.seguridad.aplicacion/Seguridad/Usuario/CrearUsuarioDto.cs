using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.comun.dominio;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace com.cpp.calypso.seguridad.aplicacion
{

    /// <summary>
    /// Representa un Usuario
    /// </summary>
    [Serializable]
    [DisplayName("Usuarios")]
    [AutoMapFrom(typeof(UsuarioDto))]
    public class CrearUsuarioDto : EntityDto
    {
        public CrearUsuarioDto()
        {
            Estado = EstadoUsuario.Activo;
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
        [EmailAddress(ErrorMessage = "Correo Electrónico Inválido")]
        public string Correo { get; set; }
  
        [Obligado]
        public EstadoUsuario Estado { get; set; }

        /// <summary>
        /// Listado de Roles que posee el Usuario
        /// </summary>
        [MinLength(1, ErrorMessage = "Debe seleccionar por menos un Rol")]
        public  string[] Roles { get; set; }

        /// <summary>
        /// Listado de Modulos que posee el Usuario
        /// </summary>
        [MinLength (1, ErrorMessage = "Debe seleccionar por menos un módulo")]
        public int[] Modulos { get; set; }

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