using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.dominio.Servicios_Proveedores;

namespace com.cpp.calypso.proyecto.aplicacion
{
    [AutoMap(typeof(Catalogo))]
    [Serializable]
    public class CatalogoDto : EntityDto
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
        public int tipo_catalogo_id { get; set; }

        public virtual TipoCatalogo tipo_catalogo { get; set; }


        [Obligado]
        [DisplayName("Predeterminado")]
        public bool predeterminado { get; set; }


        [Obligado]
        [DisplayName("Ordinal")]
        public int ordinal { get; set; }


        public bool IsDeleted { get; set; }
    }
}
