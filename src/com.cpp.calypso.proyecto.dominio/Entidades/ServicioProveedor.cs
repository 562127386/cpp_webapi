using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class ServicioProveedor : Entity, ISoftDelete
    {
        public int ServicioId { get; set; }

        public Catalogo Servicio { get; set; }

        public int ProveedorId { get; set; }

        public Proveedor Proveedor { get; set; }

        public ServicioEstado Estado { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }

    public enum ServicioEstado
    {
        [Description("Inactivo")]
        Inactivo = 0,

        [Description("Activo")]
        Activo = 1,


    }
}
