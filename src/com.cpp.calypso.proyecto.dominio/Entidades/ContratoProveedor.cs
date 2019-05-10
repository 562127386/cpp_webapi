using Abp.Domain.Entities;
using System;
using System.ComponentModel;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class ContratoProveedor : Entity, ISoftDelete
    {
        public int ProveedorId { get; set; }

        public Proveedor Proveedor { get; set; }

        public int EmpresaId { get; set; }

        public Empresa Empresa { get; set; }

        public string Codigo { get; set; }

        public string Objeto { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public int PlazoPago { get; set; }

        public decimal Monto { get; set; }

        public string OrdenCompra { get; set; }

        public ContratoEstado Estado { get; set; }

        public int? DocumentacionId { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }

    public enum ContratoEstado
    {
        [Description("Activo")]
        Activo = 1,

        [Description("Inactivo")]
        Inactivo = 0
    }
}
