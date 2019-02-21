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
using com.cpp.calypso.proyecto.dominio;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(Proveedor))]
    [Serializable]
    public class ProveedorDto : EntityDto
    {

        [Obligado]
        [DisplayName("Tipo Identificación")]
        public ProveedorTipoIdentificacion tipo_identificacion { get; set; }
        public string tipo_identificacion_nombre { get; set; }

        [Obligado]
        [StringLength(20)]
        [DisplayName("Identificación")]
        public string identificacion { get; set; }

        [Obligado]
        [StringLength(100)]
        [DisplayName("Razón Social")]
        public string razon_social { get; set; }

        [Obligado]
        [DisplayName("Contacto")]
        public int contacto_id { get; set; }


        [DisplayName("Calle Principal")]
        [StringLength(100)]
        public string calle_principal { get; set; }


        [DisplayName("Calle Secundaria")]
        [StringLength(100)]
        public string calle_secundaria { get; set; }

        [DisplayName("Referencia")]
        [StringLength(200)]
        public string referencia { get; set; }

        [StringLength(10)]
        [DisplayName("Número")]
        public string numero { get; set; }


        [DisplayName("Correo Electrónico")]
        [StringLength(50)]
        public string correo_electronico { get; set; }


        [DisplayName("Teléfono Convencional")]
        [StringLength(20)]
        public string telefono_convencional { get; set; }

        [DisplayName("Teléfono Celular")]
        [StringLength(10)]
        public string celular { get; set; }

        [Obligado]
        [DisplayName("Estado")]
        public int estado { get; set; }
        public string estado_nombre { get; set; }

        [StringLength(60)]
        public string usuario { get; set; }

        [Obligado]
        [DisplayName("Es externo?")]
        public int es_externo { get; set; }


        [StringLength(50)]
        [DisplayName("Coordenadas")]
        public string coordenadas { get; set; }


        [Obligado]
        [DisplayName("Tipo Proveedor")]
        public int tipo_proveedor_id { get; set; }

        [DisplayName("Tipo Proveedor")]
        public string tipo_proveedor_nombre { get; set; }


        [DisplayName("Código SAP")]
        public string codigo_sap { get; set; }


    }
}
