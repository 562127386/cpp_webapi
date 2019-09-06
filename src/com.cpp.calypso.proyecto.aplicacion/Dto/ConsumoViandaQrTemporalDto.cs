using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(ConsumoViandaQrTemporal))]
    [Serializable]
    public class ConsumoViandaQrTemporalDto : EntityDto
    {
        public int SolicitudViandaTemporalId { get; set; }

        public SolicitudViandaTemporal SolicitudViandaTemporal { get; set; }

        public string Identificacion { get; set; }

        public DateTime FechaConsumo { get; set; }

        public OrigenConsumoVianda? OrigenConsumoId { get; set; }

        public string SolicitudViandaTemporalRef { get; set; }

        public int Version { get; set; }

        public bool IsDeleted { get; set; }
    }
}
