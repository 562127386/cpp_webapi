using Abp.AutoMapper;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System;
using Abp.Application.Services.Dto;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMapTo(typeof(SolicitudVianda))]
    [Serializable]
    public class SolicitudViandaDto : EntityDto
    {
        public int SolicitanteId { get; set; }

        public int? LocacionId { get; set; }


        public int TipoComidaId { get; set; }

        public Catalogo TipoComida { get; set; }

        public int DisciplinaId { get; set; }

        public Catalogo Disciplina { get; set; }

        public int? AreaId { get; set; }

        public DateTime FechaSolicitud { get; set; }

        public DateTime? FechaAlcancce { get; set; }

        public int PedidoViandas { get; set; }

        public int AlcanceViandas { get; set; }

        public int TotalPedido { get; set; }

        public int Consumido { get; set; }

        public int ConsumoJustificado { get; set; }

        public int TotalConsumido { get; set; }

        public SolicitudViandaEstado Estado { get; set; }

        public int? SolicitudOriginalId { get; set; }

        public SolicitudVianda SolicitudOriginal { get; set; }

        public string ReferenciaUbicacion { get; set; }

        public string Observaciones { get; set; }

        public int? AnotadorId { get; set; }

        public DateTime? HoraEntregaRestaurante { get; set; }

        public DateTime? HoraEntregaTransportista { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
