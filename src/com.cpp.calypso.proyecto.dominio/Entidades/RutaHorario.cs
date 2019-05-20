using Abp.Domain.Entities;
using System;


namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    public class RutaHorario : Entity, ISoftDelete
    {
        public int RutaId { get; set; }

        public Ruta Ruta { get; set; }

        public TimeSpan Horario { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? UltimaModificacion { get; set; }

        public int? UsuarioCreador { get; set; }

        public int? UsuarioUltimaModificacion { get; set; }

        public DateTime? FechaEliminacion { get; set; }

        public int? UsuarioEliminacion { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
