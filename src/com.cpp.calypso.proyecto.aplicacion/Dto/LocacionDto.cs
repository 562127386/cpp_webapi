using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.dominio.Entidades;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(Locacion))]
    [Serializable]
    public class LocacionDto : EntityDto
    {
        [Obligado]
        [DisplayName("Código")]
        public int codigo { get; set; }

        [DisplayName("Zona")]
        public int ZonaId { get; set; }
        public virtual string zona_nombre { get; set; }

        [Obligado]
        [DisplayName("Nombre")]
        [LongitudMayorAttribute(200)]
        public string nombre { get; set; }


    }
}
