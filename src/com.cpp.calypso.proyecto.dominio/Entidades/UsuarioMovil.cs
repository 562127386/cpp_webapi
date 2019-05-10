using Abp.Domain.Entities;
using System;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class UsuarioMovil : Entity, ISoftDelete
    {
        public int UsuarioId { get; set; }

        public string Username { get; set; }

        public string ApellidosNombres { get; set; }

        public string Pin { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? UltimoAcceso { get; set; }

        public string Rol { get; set; }

        public bool ActivoMovil { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
