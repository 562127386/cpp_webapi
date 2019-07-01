using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System;
using System.ComponentModel.DataAnnotations;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(SolicitudPin))]
    [Serializable]
    public class SolicitudPinDto : EntityDto
    {
        public DateTime FechaSolicitud { get; set; }

        public DateTime? FechaEnvio { get; set; }

        public int? UsuarioId { get; set; }

        public string Estado { get; set; }

        public string Pin { get; set; }

        public string CorreoElectronico { get; set; }

        public string Error { get; set; }
    }
}
