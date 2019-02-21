using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class ConsumoVianda : Entity, ISoftDelete
    {
        [Obligado]
        [DisplayName("Solicitud")]
        public int SolicitudViandaId { get; set; }
        public virtual SolicitudVianda SolicitudVianda { get; set; }

        [Obligado]
        [DisplayName("Colaborador")]
        public int ColaboradorId { get; set; }
        public Colaborador colaborador { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Fecha Consumo Vianda")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fecha_consumo_vianda { get; set; }


        [Obligado]
        [DisplayName("En sitio?")]
        public int en_sitio { get; set; }

        [Obligado]
        [StringLength(500)]
        [DisplayName("Observaciones")]
        public string observaciones { get; set; }

        [Obligado]
        [DisplayName("Origen Consumo")]
        public OrigenConsumoVianda origen_consumo_id { get; set; }


        [Obligado] [DisplayName("Vigente")] public bool IsDeleted { get; set; } = true;

    }

    public enum OrigenConsumoVianda
    {
        [Description("Cédula")]
        Cedula = 1,

        [Description("QR")]
        Qr = 2,

        [Description("Huella")]
        Huella = 3
    }
}
