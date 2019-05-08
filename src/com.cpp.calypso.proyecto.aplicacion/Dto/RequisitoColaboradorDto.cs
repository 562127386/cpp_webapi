using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System;
using System.ComponentModel.DataAnnotations;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(RequisitoColaborador))]
    [Serializable]
    public class RequisitoColaboradorDto : EntityDto
    {
        [Required]
        public int RequisitoId { get; set; }

        [Required]
        public string NombreRequisito { get; set; }

        [Required]
        public bool Obligatorio { get; set; }

        [Required]
        public int ColaboradorId { get; set; }

        public Colaborador Colaborador { get; set; }

        public DateTime FechaCaducidad { get; set; }

        public bool Cumple { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
