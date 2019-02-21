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
    public class ConsumoViandaQr : Entity, ISoftDelete
    {
        [Obligado]
        [DisplayName("Solicitud")]
        public int SolicitudViandaId { get; set; }
        public virtual SolicitudVianda SolicitudVianda { get; set; }


        [Obligado]
        [DisplayName("Cédula")]
        [LongitudMayor(15)]
        public string identificacion { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Fecha Solicitud")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fecha_consumo_vianda { get; set; }

        [Obligado]
        [DisplayName("Origen Consumo")]
        public OrigenConsumoVianda origen_consumo_id { get; set; }


        public bool IsDeleted { get; set; }
    }
}
