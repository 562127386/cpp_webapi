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
using com.cpp.calypso.proyecto.dominio.Servicios_Proveedores;

namespace com.cpp.calypso.proyecto.dominio
{
    [Serializable]
    public class ConsumoQr : Entity, ISoftDelete
    {
        [Obligado]
        [DisplayName("Proveedor")]
        public int ProveedorId { get; set; }
        public virtual Proveedor Proveedor { get; set; }

        [Obligado]
        [DisplayName("Tipo Comida")]
        public int TipoComidaId { get; set; }
        public virtual Catalogo TipoComida { get; set; }

        [Obligado]
        [DisplayName("Opción Comida")]
        public int OpcionComidaId { get; set; }
        public virtual Catalogo OpcionComida { get; set; }


        [Obligado]
        [DisplayName("Cédula")]
        [LongitudMayor(15)]
        public string cedula { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Fecha Solicitud")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fecha_consumo { get; set; }

        [Obligado]
        [DisplayName("Usuario Generador")]
        public int usuario_generador { get; set; }

        [DisplayName("Origen Consumo")]
        public OrigenConsumo origen_consumo_id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
