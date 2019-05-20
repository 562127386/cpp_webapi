﻿using Abp.Domain.Entities;
using System;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    public class OperacionDiariaRuta : Entity, ISoftDelete
    {
        public int RutaHorarioVehiculoId { get; set; }

        public RutaHorarioVehiculo RutaHorarioVehiculo { get; set; }

        public int OperacionDiariaId { get; set; }

        public OperacionDiaria OperacionDiaria { get; set; }

        public string RutaHorarioVehiculoRef { get; set; }

        public string OperacionDiariaRef { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
