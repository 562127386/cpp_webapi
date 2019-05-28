using Abp.Domain.Entities;
using System;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    public class Vehiculo : Entity, ISoftDelete
    {
        public Proveedor Proveedor { get; set; }

        public int ProveedorId { get; set; }

        public string Codigo { get; set; }

        public int? CatalogoTipoVehiculoId { get; set; }

        public Catalogo CatalogoTipoVehiculo { get; set; }

        public string NumeroPlaca { get; set; }

        public string Marca { get; set; }

        public int Capacidad { get; set; }

        public int AnioFabricacion { get; set; }

        public string Color { get; set; }

        public DateTime? FechaVencimientoMatricula { get; set; }

        public string Estado { get; set; }

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
