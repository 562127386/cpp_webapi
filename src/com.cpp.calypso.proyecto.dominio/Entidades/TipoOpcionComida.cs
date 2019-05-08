using Abp.Domain.Entities;
using System;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class TipoOpcionComida : Entity, ISoftDelete
    {
        public int ContratoId { get; set; }
        public ContratoProveedor Contrato { get; set; }
        public int OpcionComidaId { get; set; }
        public Catalogo OpcionComida { get; set; }
        public decimal Costo { get; set; }
        public int TipoComidaId { get; set; }
        public Catalogo TipoComida { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
