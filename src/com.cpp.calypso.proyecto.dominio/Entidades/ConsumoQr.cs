using System;
using Abp.Domain.Entities;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class ConsumoQr : Entity, ISoftDelete
    {
        public int ProveedorId { get; set; }

        public Proveedor Proveedor { get; set; }

        public int TipoComidaId { get; set; }

        public Catalogo TipoComida { get; set; }

        public int OpcionComidaId { get; set; }

        public Catalogo OpcionComida { get; set; }

        public string Cedula { get; set; }

        public DateTime FechaConsumo { get; set; }

        public int UsuarioGeneradorId { get; set; }

        public OrigenConsumo? OrigenConsumoId { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
