using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.dominio.Servicios_Proveedores;

namespace com.cpp.calypso.proyecto.dominio
{
    [Serializable]
    public class Proveedor : Entity, ISoftDelete
    {
        public Proveedor()
        {

        }

        [Obligado]
        [DisplayName("Tipo Identificación")]
        public ProveedorTipoIdentificacion tipo_identificacion { get; set; }

        [Obligado]
        [StringLength(20)]
        [DisplayName("Identificación")]
        public string identificacion { get; set; }

        [Obligado]
        [StringLength(100)]
        [DisplayName("Razón Social")]
        public string razon_social { get; set; }


        [Obligado]
        [DisplayName("Estado")]
        public ProveedorEstado estado { get; set; }

        [Obligado]
        [DisplayName("Es externo?")]
        public int es_externo { get; set; }


        [StringLength(50)]
        [DisplayName("Coordenadas")]
        public string coordenadas { get; set; }

        [StringLength(30)]
        [DisplayName("Código SAP")]
        public string codigo_sap { get; set; }

        [DisplayName("Usuario")]
        [StringLength(60)]
        public string usuario { get; set; }


        public virtual ICollection<ContratoProveedor> contratos { get; set; }

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
