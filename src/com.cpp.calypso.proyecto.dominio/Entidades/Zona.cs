using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class Zona : Entity, ISoftDelete
    {
        [Obligado]
        [DisplayName("Codigo")]
        [LongitudMayorAttribute(10)]
        public string codigo { get; set; }

        [Obligado]
        [DisplayName("Nombre")]
        [LongitudMayorAttribute(60)]
        public string nombre { get; set; }

        [Obligado]
        [DisplayName("Descripcion")]
        [LongitudMayorAttribute(400)]
        public string descripcion { get; set; }

        [Obligado]
        [DisplayName("Vigente")]
        public bool IsDeleted { get; set; }
    }
}
