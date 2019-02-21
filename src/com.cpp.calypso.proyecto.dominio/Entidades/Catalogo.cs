using Abp.Domain.Entities;
using com.cpp.calypso.comun.dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cpp.calypso.proyecto.dominio.Servicios_Proveedores
{
    [Serializable]
    public class Catalogo : Entity, ISoftDelete
    {
        [Obligado]
        [LongitudMayor(200)]
        [DisplayName("Nombre")]
        public string nombre { get; set; }

        [Obligado]
        [LongitudMayor(20)]
        [DisplayName("Código")]
        public string codigo { get; set; }

        [Obligado]
        [LongitudMayor(800)]
        [DisplayName("Descripción")]
        public string descripcion { get; set; }

        [Obligado]
        [DisplayName("Tipo Catálogo")]
        public int TipoCatalogoId { get; set; }

        public virtual TipoCatalogo TipoCatalogo { get; set; }


        [Obligado]
        [DisplayName("Predeterminado")]
        public bool predeterminado { get; set; }


        [Obligado]
        [DisplayName("Ordinal")]
        public int ordinal { get; set; }


        public bool IsDeleted { get; set; }
    }
}
