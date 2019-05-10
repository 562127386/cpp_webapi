using System;
using Abp.Domain.Entities;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class Locacion : Entity, ISoftDelete
    {
        public int Codigo { get; set; }
        public int ZonaId { get; set; }
        public Zona Zona { get; set; }
        public string Nombre { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
