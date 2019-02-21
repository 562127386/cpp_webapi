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
using com.cpp.calypso.proyecto.dominio.Entidades;
using com.cpp.calypso.proyecto.dominio.Servicios_Proveedores;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(Consumo))]
    [Serializable]
    public class ConsumoDto : EntityDto
    {
        [Obligado]
        [DisplayName("Proveedor")]
        public int ProveedorId { get; set; }
        public virtual Proveedor Proveedor { get; set; }


        [Obligado]
        [DisplayName("Colaborador")]
        public int ColaboradorId { get; set; }
        public Colaborador colaborador { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fecha { get; set; }

        [Obligado]
        [DisplayName("Tipo Comida")]
        public int TipoComidaId { get; set; }
        public virtual Catalogo TipoComida { get; set; }

        [Obligado]
        [DisplayName("Opción Comida")]
        public int OpcionComidaId { get; set; }
        public virtual Catalogo OpcionComida { get; set; }

        [Obligado]
        [StringLength(500)]
        [DisplayName("Observación")]
        public string observacion { get; set; }

        [DisplayName("Origen Consumo")]
        public OrigenConsumo origen_consumo_id { get; set; }

        [Obligado]
        [DisplayName("Vigente")]
        public bool IsDeleted { get; set; } = true;
    }
}
