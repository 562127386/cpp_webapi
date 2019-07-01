using Abp.Domain.Entities;
using System;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class EspacioHabitacion : Entity, ISoftDelete
    {
        public int? HabitacionId { get; set; }

        public Habitacion Habitacion { get; set; }

        public string HabitacionRef { get; set; }

        public string CodigoEspacio { get; set; }

        public bool Estado { get; set; }

        public bool Activo { get; set; }

        public string Observaciones { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

        public string uid { get; set; }

        public long? CreatorUserId { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
