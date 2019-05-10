using Abp.Domain.Entities;
using System;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class Archivo : Entity, ISoftDelete
    {
        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string Hash { get; set; }

        public string TipoContenido { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
