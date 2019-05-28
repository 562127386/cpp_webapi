using System;
using System.ComponentModel;
using Abp.Domain.Entities;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class Consumo : Entity, ISoftDelete
    {
        public int ProveedorId { get; set; }

        public Proveedor Proveedor { get; set; }

        public int ColaboradorId { get; set; }

        public Colaborador Colaborador { get; set; }

        public DateTime Fecha { get; set; }

        public int TipoComidaId { get; set; }

        public Catalogo TipoComida { get; set; }

        public int OpcionComidaId { get; set; }

        public Catalogo OpcionComida { get; set; }

        public string Observacion { get; set; }

        public OrigenConsumo? OrigenConsumoId { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

        public string uid { get; set; }
    }

    public enum OrigenConsumo
    {
        [Description("Cédula")]
        Cedula = 1,

        [Description("QR")]
        Qr = 2,

        [Description("Huella")]
        Huella = 3
    }
}
