using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.comun.dominio;
using System;
using System.ComponentModel;

namespace com.cpp.calypso.seguridad.aplicacion
{



    [Serializable]
    [AutoMap(typeof(Modulo))]
    [DisplayName("Gestión de Modulos")]
    public class ModuloDto : EntityDto
    {
        [Obligado]
        [LongitudMayor(60)]
        public string Codigo { get; set; }

        [Obligado]
        [LongitudMayor(80)]
        public string Nombre { get; set; }


        [DisplayName("Descripción")]
        [LongitudMayor(255)]
        public string Descripcion { get; set; }

        public override string ToString()
        {
            return Nombre;
        }

    }
}