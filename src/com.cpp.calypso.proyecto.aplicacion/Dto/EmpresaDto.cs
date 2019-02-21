using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.dominio.Entidades;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(Empresa))]
    [Serializable]
    public class EmpresaDto : EntityDto
    {
        [Obligado]
        [DisplayName("Tipo de Identificación")]
        public TipoDeIdentificacion tipo_identificacion { get; set; }

        [LongitudMayor(20)]
        [Obligado]
        [DisplayName("Identificación")]
        public string identificacion { get; set; }

        [LongitudMayor(100)]
        [Obligado]
        [DisplayName("Razón Social")]
        public string razon_social { get; set; }


        [LongitudMayor(255)]
        [Obligado]
        [DisplayName("Dirección")]
        public string direccion { get; set; }


        [LongitudMayor(100)]
        [Obligado]
        [DisplayName("Correo Electrónico")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string correo { get; set; }

        [Obligado]
        [DefaultValue(true)]
        [DisplayName("Estado(Activo/Inactivo)")]
        public bool estado { get; set; }


        [LongitudMayor(15)]
        [Obligado]
        [DisplayName("Teléfono")]
        public string telefono { get; set; }


        [Obligado]
        [DisplayName("Tipo de Sociedad")]
        public TipoDeSociedad tipo_sociedad { get; set; }

        [LongitudMayor(800)]
        [DisplayName("Observaciones")]
        public string observaciones { get; set; }


        [Obligado]
        [DisplayName("Es Principal?")]
        public bool es_principal { get; set; }


        [Obligado]
        [DisplayName("Tipo de Contribuyente")]
        public TipoDeContribuyente tipo_contribuyente { get; set; }


        [Obligado]
        [DefaultValue(true)]
        public bool vigente { get; set; }


        [DisplayName("Código Sap")]
        [LongitudMayor(200)]
        public string codigo_sap { get; set; }


        public bool IsDeleted { get; set; }
    }
}
