using System;
using System.Collections.Generic;
using System.ComponentModel;
using Abp.Domain.Entities;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class Proveedor : Entity, ISoftDelete
    {
        public ProveedorTipoIdentificacion TipoIdentificacion { get; set; }

        public string Identificacion { get; set; }

        public string RazonSocial { get; set; }

        public ProveedorEstado Estado { get; set; }

        public int EsExterno { get; set; }

        public string Coordenadas { get; set; }

        public string CodigoSap { get; set; }

        public string Usuario { get; set; }

        public virtual ICollection<ContratoProveedor> Contratos { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }

    public enum ProveedorTipoIdentificacion
    {
        Cedula = 1,
        Ruc = 2,
        Passaporte = 3,
    }

    public enum ProveedorEstado
    {
        [Description("Activo")]
        Activo = 1,

        [Description("Inactivo")]
        Inactivo = 0
    }
}
