using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(ReservaHotel))]
    [Serializable]
    public class ReservaHotelDto : EntityDto
    {
        public int EspacioHabitacionId { get; set; }

        public EspacioHabitacion EspacioHabitacion { get; set; }

        public int ColaboradorId { get; set; }

        public Colaborador Colaborador { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public DateTime? FechaDesde { get; set; }

        public DateTime? FechaHasta { get; set; }

        public bool Estado { get; set; }

        public string IdentificacionColaborador { get; set; }

        public string Nombres { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }


        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

        public string uid { get; set; }
    }
}
