using System;
using System.ComponentModel;
using Abp.Domain.Entities;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class ConsumoVianda : Entity, ISoftDelete
    {
        public int SolicitudViandaId { get; set; }
        public SolicitudVianda SolicitudVianda { get; set; }
        public int ColaboradorId { get; set; }
        public Colaborador Colaborador { get; set; }
        public DateTime FechaConsumoVianda { get; set; }
        public int EnSitio { get; set; }
        public string Observaciones { get; set; }
        public OrigenConsumoVianda OrigenConsumoId { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

    }

    public enum OrigenConsumoVianda
    {
        [Description("Cédula")]
        Cedula = 1,

        [Description("QR")]
        Qr = 2,

        [Description("Huella")]
        Huella = 3
    }
}
