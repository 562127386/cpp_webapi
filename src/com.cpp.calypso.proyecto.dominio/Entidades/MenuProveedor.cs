using System;
using Abp.Domain.Entities;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class MenuProveedor : Entity
    {
        public int ProveedorId { get; set; }

        public Proveedor Proveedor { get; set; }

        public DateTime FechaInicial { get; set; }

        public DateTime FechaFinal { get; set; }

        public bool Aprobado { get; set; }

        public DateTime? FechaAprobacion { get; set; }

        public string Descripcion { get; set; }

        public int? DocumentacionId { get; set; }

        public string DocumentacionRef { get; set; }

        public Archivo Documentacion { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
