using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.dominio;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(ContratoProveedor))]
    [Serializable]
    public class ContratoProveedorDto : EntityDto
    {

        [Obligado]
        [DisplayName("Proveedor")]
        public int ProveedorId { get; set; }

        [DisplayName("Código")]
        public string codigo { get; set; }

        [Obligado]
        [DisplayName("Empresa")]
        public int EmpresaId { get; set; }

        [DisplayName("Cliente")]
        public string empresa_nombre { get; set; }

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
        public string estado_nombre { get; set; }
    }
}
