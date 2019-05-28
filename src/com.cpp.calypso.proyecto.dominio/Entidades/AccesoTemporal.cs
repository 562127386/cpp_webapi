using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    public class AccesoTemporal : Entity, ISoftDelete
    {
        [Required]
        public string Identificacion { get; set; }

        [Required]
        public DateTime FechaAcceso { get; set; }

        [Required]
        public int UsuarioCreadorId { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

        public string uid { get; set; }
    }
}
