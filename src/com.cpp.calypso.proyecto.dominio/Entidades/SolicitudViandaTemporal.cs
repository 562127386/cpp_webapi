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
    public class SolicitudViandaTemporal : Entity, ISoftDelete
    {
        public DateTime FechaSolicitud { get; set; }

        public int SolicitanteId { get; set; }

        [Obligado]
        [DisplayName("Tipo Catálogo")]
        public int TipoComidaId { get; set; }

        public Catalogo TipoComida { get; set; }

        public int TotalPedido { get; set; }

        public int TotalConsumido { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

        public string uid { get; set; }
    }
}
