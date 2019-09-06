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
    [AutoMap(typeof(ConsumoViandaTemporal))]
    [Serializable]
    public class ConsumoViandaTemporalDto : EntityDto
    {
        public int SolicitudViandaTemporalId { get; set; }

        public SolicitudViandaTemporal SolicitudViandaTemporal { get; set; }

        public DateTime FechaConsumo { get; set; }

        public int ColaboradorId { get; set; }

        public OrigenConsumo? OrigenConsumoId { get; set; }

        public string SolicitudViandaTemporalRef { get; set; }

        public int Version { get; set; }

        public bool IsDeleted { get; set; }
    }
}
