using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class SolicitudReporteMovil : Entity
    {
        public string CodigoReporte { get; set; }

        public DateTime FechaSolicitud { get; set; }

        public DateTime? FechaEnvio { get; set; }

        public int UsuarioId { get; set; }

        public string Estado { get; set; }

        public string Parametros { get; set; }

        public string CorreoElectronico { get; set; }

        public string Error { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public string uid { get; set; }
    }
}
