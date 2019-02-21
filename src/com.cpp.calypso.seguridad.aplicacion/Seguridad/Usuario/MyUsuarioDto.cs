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
    [AutoMapTo(typeof(Usuario))]
    public class MyUsuarioDto : EntityDto
    {
        public MyUsuarioDto()
        {
             
        }


    
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