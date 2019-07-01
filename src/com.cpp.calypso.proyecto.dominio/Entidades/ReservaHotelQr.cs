using Abp.Domain.Entities;
using System;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class ReservaHotelQr : Entity, ISoftDelete
    {
        public int EspacioHabitacionId { get; set; }

        public EspacioHabitacion EspacioHabitacion { get; set; }

        public string IdentificacionColaborador { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public DateTime? FechaDesde { get; set; }

        public DateTime? FechaHasta { get; set; }


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
