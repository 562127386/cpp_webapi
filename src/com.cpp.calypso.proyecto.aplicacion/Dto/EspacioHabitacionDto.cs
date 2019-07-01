using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(EspacioHabitacion))]
    [Serializable]
    public class EspacioHabitacionDto : EntityDto
    {
        public int HabitacionId { get; set; }

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
    }
}
