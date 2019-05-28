using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    public class RequisitoColaborador : Entity, ISoftDelete
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

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

        public string uid { get; set; }
    }
}
