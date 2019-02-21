using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.dominio.Entidades;

namespace com.cpp.calypso.proyecto.dominio
{
    [Serializable]
    public class ContratoProveedor : Entity, ISoftDelete
    {
        [Obligado]
        [DisplayName("Proveedor")]
        public int ProveedorId { get; set; }
        public virtual Proveedor Proveedor { get; set; }

        [Obligado]
        [DisplayName("Empresa")]
        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }

        [Obligado]
        [StringLength(30)]
        [DisplayName("Código")]
        public string codigo { get; set; }


        [Obligado]
        [StringLength(500)]
        [DisplayName("Objeto")]
        public string objeto { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Fecha inicio")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fecha_inicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Fecha fin")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fecha_fin { get; set; }

        [Obligado]
        [DisplayName("Plazo Pago")]
        public int plazo_pago { get; set; }

        [Obligado]
        [DisplayName("Monto")]
        public decimal monto { get; set; }

        [Obligado]
        [StringLength(10)]
        [DisplayName("Orden de Compra")]
        public string orden_compra { get; set; }

        [Obligado]
        [DisplayName("Estado")]
        public ContratoEstado estado { get; set; }

        [DisplayName("Documentación")]
        public int? documentacion_id { get; set; }

        public virtual ICollection<TipoOpcionComida> tipo_opciones_comida { get; set; }

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
