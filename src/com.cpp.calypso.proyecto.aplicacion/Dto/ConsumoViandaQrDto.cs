using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using com.cpp.calypso.comun.aplicacion;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(ConsumoViandaQr))]
    [Serializable]
    public class ConsumoViandaQrDto : EntityDto
    {
        public int SolicitudViandaId { get; set; }
        public SolicitudViandaDto SolicitudVianda { get; set; }
        public string Identificacion { get; set; }
        public DateTime FechaConsumoVianda { get; set; }
        public OrigenConsumoVianda OrigenConsumoId { get; set; }
        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
