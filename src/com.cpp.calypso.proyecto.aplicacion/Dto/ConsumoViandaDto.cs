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
    [AutoMap(typeof(ConsumoVianda))]
    [Serializable]
    public class ConsumoViandaDto : EntityDto
    {

        [Obligado]
        [DisplayName("Solicitud")]
        public int SolicitudViandaId { get; set; }

        [Obligado]
        [DisplayName("Colaborador")]
        public int ColaboradorId { get; set; }
        public virtual string colaborador_nombre { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Fecha Consumo Vianda")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fecha_consumo_vianda { get; set; }

        [Obligado]
        [DisplayName("Tipo Comida")]
        public int TipoOpcionComidaId { get; set; }


        [Obligado]
        [DisplayName("En sitio?")]
        public int en_sitio { get; set; }

        [Obligado]
        [StringLength(500)]
        [DisplayName("Observaciones")]
        public string observaciones { get; set; }


    }
}
