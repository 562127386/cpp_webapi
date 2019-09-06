using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class ConsumoViandaTemporal : Entity, ISoftDelete
    {
        public int SolicitudViandaTemporalId { get; set; }

        public SolicitudViandaTemporal SolicitudViandaTemporal { get; set; }

        public DateTime FechaConsumo { get; set; }

        public int ColaboradorId { get; set; }

        public OrigenConsumo? OrigenConsumoId { get; set; }

        public string SolicitudViandaTemporalRef { get; set; }


        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public bool IsDeleted { get; set; }

        public string uid { get; set; }
    }
}
