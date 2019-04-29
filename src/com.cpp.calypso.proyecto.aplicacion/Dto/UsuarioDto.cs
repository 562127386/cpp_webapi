using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.comun.dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [Serializable]
    [DisplayName("Usuarios")]
    [AutoMap(typeof(Usuario))]
    public class UsuarioDto : EntityDto
    {
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
