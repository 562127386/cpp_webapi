using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using com.cpp.calypso.comun.dominio;
using Newtonsoft.Json;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class Empresa : Entity, ISoftDelete
    {
        [Obligado]
        [DisplayName("Tipo de Identificación")]
        public TipoDeIdentificacion tipo_identificacion { get; set; }

        [LongitudMayorAttribute(20)]
        [Obligado]
        [DisplayName("Identificación")]
        public string identificacion { get; set; }

        [LongitudMayorAttribute(100)]
        [Obligado]
        [DisplayName("Razón Social")]
        public string razon_social { get; set; }


        [LongitudMayorAttribute(255)]
        [Obligado]
        [DisplayName("Dirección")]
        public string direccion { get; set; }


        [LongitudMayorAttribute(100)]
        [Obligado]
        [DisplayName("Correo Electrónico")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string correo { get; set; }

        [Obligado]
        [DefaultValue(true)]
        [DisplayName("Estado(Activo/Inactivo)")]
        public bool estado { get; set; }


        [LongitudMayorAttribute(15)]
        [Obligado]
        [DisplayName("Teléfono")]
        public string telefono { get; set; }


        [Obligado]
        [DisplayName("Tipo de Sociedad")]
        public TipoDeSociedad tipo_sociedad { get; set; }

        [LongitudMayorAttribute(800)]
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
        [LongitudMayorAttribute(200)]
        public string codigo_sap { get; set; }


        public bool IsDeleted { get; set; }
    }

    public enum TipoDeIdentificacion
    {
        RUC, // Ruc = 1
        Cedula, // Cedula = 2
        Passaporte, // Passaporte = 3
    }

    public enum TipoDeSociedad
    {
        Normal,
        Especial,
    }

    public enum TipoDeContribuyente
    {
        Normal,
        Especial,
    }
}
