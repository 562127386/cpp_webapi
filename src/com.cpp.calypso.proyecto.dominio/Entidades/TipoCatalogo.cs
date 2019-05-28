using System;
using Abp.Domain.Entities;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class TipoCatalogo : Entity, ISoftDelete
    {
        public string Nombre { get; set; }

        public string Codigo { get; set; }

        public string TipoOrdenamiento { get; set; }

        public bool Editable { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

        public string uid { get; set; }
    }
}
