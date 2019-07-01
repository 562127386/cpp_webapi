using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using com.cpp.calypso.comun.aplicacion;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(ReporteMovil))]
    [Serializable]
    public class ReporteMovilDto : EntityDto
    {
        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public string Parametros { get; set; }

        public int CatalogoServicioId { get; set; }

        public Catalogo CatalogoServicio { get; set; }

        public string Rol { get; set; }

        public DateTime? fs { get; set; }

        public DateTime? fr { get; set; }

        public string uid { get; set; }
    }
}
