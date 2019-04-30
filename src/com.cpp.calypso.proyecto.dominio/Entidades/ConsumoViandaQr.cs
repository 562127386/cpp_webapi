using Abp.Domain.Entities;
using System;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class ConsumoViandaQr : Entity, ISoftDelete
    {
        public int SolicitudViandaId { get; set; }

        public SolicitudVianda SolicitudVianda { get; set; }

        public string Identificacion { get; set; }

        public DateTime FechaConsumoVianda { get; set; }

        public OrigenConsumoVianda? OrigenConsumoId { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
