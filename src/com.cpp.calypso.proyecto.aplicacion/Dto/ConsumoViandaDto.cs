using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using com.cpp.calypso.comun.aplicacion;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(ConsumoVianda))]
    [Serializable]
    public class ConsumoViandaDto : EntityDto
    {

        public int SolicitudViandaId { get; set; }
        public SolicitudViandaDto SolicitudVianda { get; set; }
        public int ColaboradorId { get; set; }
        public ColaboradorDto Colaborador { get; set; }
        public DateTime FechaConsumoVianda { get; set; }
        public int EnSitio { get; set; }
        public string Observaciones { get; set; }
        public OrigenConsumoVianda OrigenConsumoId { get; set; }
        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }


    }
}
