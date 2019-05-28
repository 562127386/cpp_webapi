using Abp.Domain.Entities;
using System;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class TipoAccionEmpresa : Entity, ISoftDelete
    {
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public int TipoComidaId { get; set; }
        public Catalogo TipoComida { get; set; }
        public int AccionId { get; set; }
        public Catalogo Accion { get; set; }
        public DateTime HoraDesde { get; set; }
        public DateTime HoraHasta { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

        public string uid { get; set; }

    }
}
