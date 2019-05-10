using Abp.Domain.Entities;
using System;
using System.ComponentModel;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class SolicitudVianda : Entity, ISoftDelete
    {
        public int SolicitanteId { get; set; }

        public Colaborador Solicitante { get; set; }

        public int LocacionId { get; set; }

        public Locacion Locacion { get; set; }

        public int TipoComidaId { get; set; }

        public Catalogo TipoComida { get; set; }

        public int DisciplinaId { get; set; }

        public Catalogo Disciplina { get; set; }

        public int AreaId { get; set; }

        public Catalogo Area { get; set; }

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

        public Colaborador Anotador { get; set; }

        public DateTime? HoraEntregaRestaurante { get; set; }

        public DateTime? HoraEntregaTransportista { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }

    public enum SolicitudViandaEstado
    {
        [Description("Cancelado")]
        Cancelado = 0,

        [Description("Registrado")]
        Registrado = 1,

        [Description("Asignada")]
        Asignada = 2,

        [Description("Aprobada")]
        Aprobada = 3,

        [Description("Asignado Transporte")]
        AsignadaTransporte = 4,

        [Description("Despachada Transporte")]
        DespachadaTransporte = 5,

        [Description("Entregada Anotador")]
        EntregadaAnotador = 6,

        [Description("Justificada")]
        Justificada = 7,

        [Description("Liquilado")]
        Liquilado = 8
    }
}
