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
    public class Locacion : Entity, ISoftDelete
    {
        [Obligado]
        [DisplayName("Código")]
        public int codigo { get; set; }

        [DisplayName("Zona")]
        public int ZonaId { get; set; }
        public virtual Zona Zona { get; set; }

        [Obligado]
        [DisplayName("Nombre")]
        [LongitudMayorAttribute(200)]
        public string nombre { get; set; }

        [Obligado]
        [DisplayName("Vigente")]
        public bool IsDeleted { get; set; }
    }
}
