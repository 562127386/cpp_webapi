using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(Habitacion))]
    [Serializable]
    public class HabitacionDto : EntityDto
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
    }
}
