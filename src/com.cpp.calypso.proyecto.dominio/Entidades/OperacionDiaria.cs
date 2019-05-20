using Abp.Domain.Entities;
using System;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    public class OperacionDiaria : Entity, ISoftDelete
    {
        public int? VehiculoId { get; set; }

        public Vehiculo Vehiculo { get; set; }

        public int? ChoferId { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public int KilometrajeInicio { get; set; }

        public int KilometrajeFinal { get; set; }

        public string Observacion { get; set; }

        public string Estado { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
