using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(Archivo))]
    [Serializable]
    public class ArchivoDto : EntityDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Hash { get; set; }
        public string TipoContenido { get; set; }
        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
