using System;
using System.ComponentModel;
using Abp.Domain.Entities;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class SincronizacionLog : Entity
    {
        public int UsuarioId { get; set; }

        public string Log { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
