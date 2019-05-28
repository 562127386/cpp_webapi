using Abp.Domain.Entities;
using System;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    public class RutaHorarioVehiculo : Entity, ISoftDelete
    {
        public int RutaHorarioId { get; set; }

        public RutaHorario RutaHorario { get; set; }

        public int? VehiculoId { get; set; }

        public Vehiculo Vehiculo { get; set; }

        public DateTime? FechaDesde { get; set; }

        public DateTime? FechaHasta { get; set; }

        public TimeSpan HoraSalida { get; set; }

        public TimeSpan HoraLlegada { get; set; }

        public int Duracion { get; set; }

        public string Observacion { get; set; }

        public string Estado { get; set; }

        public DateTime? FechaAsignacion { get; set; }

        public int? UsuarioAsignacion { get; set; }

        public DateTime? FechaDesasignacion { get; set; }

        public int? UsuarioDesasignacion { get; set; }

        public bool CreaOperacion { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? UltimaModificacion { get; set; }

        public int? UsuarioCreador { get; set; }

        public int? UsuarioUltimaModificacion { get; set; }

        public DateTime? FechaEliminacion { get; set; }

        public int? UsuarioEliminacion { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

        public string uid { get; set; }
    }
}
