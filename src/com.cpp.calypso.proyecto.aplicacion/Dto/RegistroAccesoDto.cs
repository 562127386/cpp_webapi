using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System;
using System.ComponentModel.DataAnnotations;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(RegistroAcceso))]
    [Serializable]
    public class RegistroAccesoDto : EntityDto
    {
        [Required]
        public int ColaboradorId { get; set; }

        public Colaborador Colaborador { get; set; }

        [Required]
        public DateTime FechaAcceso { get; set; }

        [Required]
        public int TipoAccesoId { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
