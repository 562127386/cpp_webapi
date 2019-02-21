using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Servicios_Proveedores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.proyecto.aplicacion
{
    [AutoMap(typeof(TipoCatalogo))]
    [Serializable]
    public class TipoCatalogoDto : EntityDto
    {
        [Obligado]
        [LongitudMayor(200)]
        [DisplayName("Nombre")]
        public string nombre { get; set; }

        [Obligado]
        [LongitudMayor(50)]
        [DisplayName("Código")]
        public string codigo { get; set; }


        [Obligado]
        [LongitudMayor(3)]
        [DisplayName("Tipo Ordinal")]
        public string tipo_ordenamiento { get; set; }


        [Obligado]
        public bool editable { get; set; }


        public bool IsDeleted { get; set; }
    }
}
