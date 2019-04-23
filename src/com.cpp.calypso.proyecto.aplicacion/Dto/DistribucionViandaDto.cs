using Abp.AutoMapper;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System;
using Abp.Application.Services.Dto;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(DistribucionVianda))]
    [Serializable]
    public class DistribucionViandaDto : EntityDto
    {
        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
        public int TipoComidaId { get; set; }
        public Catalogo TipoComida { get; set; }
        public int Estado { get; set; }
        public DateTime Fecha { get; set; }
        public int TotalPedido { get; set; }
        public int TotalEntregadoTransporte { get; set; }
        public int TotalJustificado { get; set; }
        public int TotalLiquidado { get; set; }
        public int Liquidado { get; set; }
        public int ConductorAsigandoId { get; set; }
        public Colaborador ConductorAsigando { get; set; }
        public DateTime HoraAsigancionTransporte { get; set; }
        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

    }
}
