using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class Habitacion : Entity, ISoftDelete
    {
        public int ProveedorId { get; set; }

        public Proveedor Proveedor { get; set; }

        public string NumeroHabitacion { get; set; }

        public int TipoHabitacionId { get; set; }

        public Catalogo TipoHabitacion { get; set; }

        public int Capacidad { get; set; }

        public bool Estado { get; set; }

        public bool Aprobado { get; set; }

        public DateTime? FechaAprobacion { get; set; }

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
