using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.dominio.Entidades;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(SolicitudViandaTemporal))]
    [Serializable]
    public class SolicitudViandaTemporalDto : EntityDto
    {
        public DateTime FechaSolicitud { get; set; }

        public int SolicitanteId { get; set; }

        [Obligado]
        [DisplayName("Tipo Catálogo")]
        public int TipoComidaId { get; set; }

        public Catalogo TipoComida { get; set; }

        public int TotalPedido { get; set; }

        public int TotalConsumido { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

    }
}
