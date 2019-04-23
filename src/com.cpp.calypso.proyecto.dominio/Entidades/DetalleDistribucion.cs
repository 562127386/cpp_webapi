using System;
using Abp.Domain.Entities;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class DetalleDistribucion : Entity, ISoftDelete
    {
        public int DistribucionViandaId { get; set; }

        public DistribucionVianda DistribucionVianda { get; set; }

        public int SolicitudViandaId { get; set; }

        public SolicitudVianda SolicitudVianda { get; set; }

        public int TotalAsignado { get; set; }

        public int TotalEntregado { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

    }
}
