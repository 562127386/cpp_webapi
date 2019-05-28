using com.cpp.calypso.comun.dominio;
using System;
using System.ComponentModel;
using Abp.Domain.Entities;

namespace com.cpp.calypso.proyecto.dominio.Entidades
{
    [Serializable]
    public class Catalogo : Entity, ISoftDelete
    {

        [Obligado]
        [LongitudMayor(200)]
        [DisplayName("Nombre")]
        public string Nombre { get; set; }

        [Obligado]
        [LongitudMayor(20)]
        [DisplayName("Código")]
        public string Codigo { get; set; }

        [Obligado]
        [LongitudMayor(800)]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [Obligado]
        [DisplayName("Tipo Catálogo")]
        public int TipoCatalogoId { get; set; }

        public virtual TipoCatalogo TipoCatalogo { get; set; }


        [Obligado]
        [DisplayName("Predeterminado")]
        public bool Predeterminado { get; set; }


        [Obligado]
        [DisplayName("Ordinal")]
        public int Ordinal { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

        public string uid { get; set; }
    }
}
