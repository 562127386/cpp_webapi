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
    [AutoMapTo(typeof(SolicitudVianda))]
    [Serializable]
    public class SolicitudViandaDto : EntityDto
    {
        [Obligado]
        [DisplayName("Solicitante")]
        public int solicitante_id { get; set; }

        [DisplayName("Solicitante")]
        public string solicitante_nombre { get; set; }

        [Obligado]
        [DisplayName("Locacion")]
        public int LocacionId { get; set; }
        public string locacion_nombre { get; set; }


        public string zona_nombre { get; set; }

        [Obligado]
        [DisplayName("Tipo Comida")]
        public int tipo_comida_id { get; set; }
        public virtual string tipo_comida_nombre { get; set; }

        [Obligado]
        [DisplayName("Disciplina")]
        public int disciplina_id { get; set; }
        public virtual string disciplina_nombre { get; set; }


        [Obligado]
        [DisplayName("Area")]
        public int area_id { get; set; }
        public virtual string area_nombre { get; set; }

        [Obligado]
        [DataType(DataType.Date)]
        [DisplayName("Fecha Solicitud")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fecha_solicitud { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Fecha Alcance")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? fecha_alcancce { get; set; }

        [Obligado]
        [DisplayName("Pedido Viandas")]
        public int pedido_viandas { get; set; }

        [DisplayName("Alcance Viandas")]
        public int alcance_viandas { get; set; }

        [Obligado]
        [DisplayName("Total Pedido")]
        public int total_pedido { get; set; }


        [DisplayName("Consumido")]
        public int consumido { get; set; }


        [DisplayName("Consumo Justificado")]
        public int consumo_justificado { get; set; }


        [DisplayName("Total Consumido")]
        public int total_consumido { get; set; }



        [Obligado]
        [DisplayName("Estado")]
        public SolicitudViandaEstado estado { get; set; }

        [DisplayName("Solicitud Original")]
        public int? solicitud_original_id { get; set; }

        [Obligado]
        [LongitudMayorAttribute(200)]
        [DisplayName("Referencia Ubicación")]
        public string referencia_ubicacion { get; set; }

        [LongitudMayor(255)]
        [DisplayName("observaciones")]
        public string observaciones { get; set; }
    }
}
