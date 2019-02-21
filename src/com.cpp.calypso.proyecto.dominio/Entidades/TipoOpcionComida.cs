using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.dominio.Servicios_Proveedores;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class TipoOpcionComida : Entity, ISoftDelete
    {
        [Obligado]
        [DisplayName("Contrato")]
        public int ContratoId { get; set; }
        public virtual ContratoProveedor contrato { get; set; }


        [Obligado]
        [DisplayName("Opciones Comida")]
        public int opcion_comida_id { get; set; }

        public virtual Catalogo opcion_comida { get; set; }


        [Obligado]
        [DisplayName("Costo")]
        public decimal costo { get; set; }

        [Obligado]
        [DisplayName("Tipo Comida")]
        public int tipo_comida_id { get; set; }

        public virtual Catalogo tipo_comida { get; set; }

        public bool IsDeleted { get; set; }
    }
}
