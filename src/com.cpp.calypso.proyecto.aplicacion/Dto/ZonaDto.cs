using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using com.cpp.calypso.comun.aplicacion;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(Zona))]
    [Serializable]
    public class ZonaDto : EntityDto
    {
        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
