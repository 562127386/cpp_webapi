using Abp.AutoMapper;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System;
using Abp.Application.Services.Dto;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(DetalleDistribucion))]
    [Serializable]
    public class DetalleDistribucionDto : EntityDto
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
