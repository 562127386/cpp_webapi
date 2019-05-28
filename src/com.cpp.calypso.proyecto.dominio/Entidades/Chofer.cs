using Abp.Domain.Entities;
using System;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    public class Chofer : Entity, ISoftDelete
    {
        public Proveedor Proveedor { get; set; }

        public int ProveedorId { get; set; }

        public int? CatalogoTipoIdentificacionId { get; set; }

        public Catalogo CatalogoTipoIdentificacion { get; set; }

        public string NumeroIdentificacion { get; set; }

        public string ApellidosNombres { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public int? CatalogoGeneroId { get; set; }

        public Catalogo CatalogoGenero { get; set; }

        public int? CatalogoEstadoCivilId { get; set; }

        public Catalogo CatalogoEstadoCivil { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public string Celular { get; set; }

        public string Mail { get; set; }

        public int Estado { get; set; }

        public DateTime? FechaEstado { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? UltimaModificacion { get; set; }

        public DateTime? FechaEliminacion { get; set; }

        public int? UsuarioEliminacion { get; set; }

        public int? UsuarioCreador { get; set; }

        public int? UsuarioUltimaModificacion { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

        public string uid { get; set; }
    }
}
