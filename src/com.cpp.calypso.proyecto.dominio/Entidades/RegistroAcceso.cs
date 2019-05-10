using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    public class RegistroAcceso : Entity, ISoftDelete
    {
        [Required]
        public int ColaboradorId { get; set; }

        public Colaborador Colaborador { get; set; }

        [Required]
        public DateTime FechaAcceso { get; set; }

        [Required]
        public int TipoAccesoId { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
