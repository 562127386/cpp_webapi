using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.dominio.Servicios_Proveedores;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class SolicitudVianda : AuditedEntity, ISoftDelete
    {
        [Obligado]
        [DisplayName("Solicitante")]
        public int solicitante_id { get; set; }
        public Colaborador solicitante { get; set; }


        [Obligado]
        [DisplayName("Locacion")]
        public int LocacionId { get; set; }
        public virtual Locacion locacion { get; set; }

        [Obligado]
        [DisplayName("Tipo Comida")]
        public int tipo_comida_id { get; set; }
        public virtual Catalogo tipo_comida { get; set; }

        [Obligado]
        [DisplayName("Disciplina")]
        public int disciplina_id { get; set; }
        public virtual Catalogo disciplina { get; set; }

        [Obligado]
        [DisplayName("Area")]
        public int area_id { get; set; }
        public virtual Catalogo area { get; set; }

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

        [Obligado]
        [DisplayName("Alcance Viandas")]
        public int alcance_viandas { get; set; }

        [Obligado]
        [DisplayName("Total Pedido")]
        public int total_pedido { get; set; }

        [Obligado]
        [DisplayName("Consumido")]
        public int consumido { get; set; }

        [Obligado]
        [DisplayName("Consumo Justificado")]
        public int consumo_justificado { get; set; }

        [Obligado]
        [DisplayName("Total Consumido")]
        public int total_consumido { get; set; }


        [Obligado]
        [DisplayName("Estado")]
        public SolicitudViandaEstado estado { get; set; }


        [DisplayName("Solicitud Original")]
        public int? solicitud_original_id { get; set; }

        [LongitudMayor(200)]
        [DisplayName("Referencia Ubicación")]
        public string referencia_ubicacion { get; set; }

        [LongitudMayor(255)]
        [DisplayName("observaciones")]
        public string observaciones { get; set; }

        [DisplayName("Anotador")]
        public int? anotador_id { get; set; }
        public Colaborador anotador { get; set; }


        [DataType(DataType.Date)]
        [DisplayName("Hora Entrega Restaurante")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? hora_entrega_restaurante { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Hora Entrega Transportista")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? hora_entrega_transportista { get; set; }


        public bool IsDeleted { get; set; }
    }

    public enum SolicitudViandaEstado
    {
        [Description("Cancelado")]
        Cancelado = 0,

        [Description("Registrado")]
        Registrado = 1,

        [Description("Asignada")]
        Asignada = 2,

        [Description("Aprobada")]
        Aprobada = 3,

        [Description("Asignado Transporte")]
        AsignadaTransporte = 4,

        [Description("Despachada Transporte")]
        DespachadaTransporte = 5,

        [Description("Entregada Anotador")]
        EntregadaAnotador = 6,

        [Description("Justificada")]
        Justificada = 7,

        [Description("Liquilado")]
        Liquilado = 8
    }
}
