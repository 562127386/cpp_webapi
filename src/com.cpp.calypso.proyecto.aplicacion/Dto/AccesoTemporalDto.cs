using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System;
using System.ComponentModel.DataAnnotations;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(AccesoTemporal))]
    [Serializable]
    public class AccesoTemporalDto : EntityDto
    {
        [Required]
        public string Identificacion { get; set; }

        [Required]
        public DateTime FechaAcceso { get; set; }

        [Required]
        public int UsuarioCreadorId { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
