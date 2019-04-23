using Abp.AutoMapper;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System;
using Abp.Application.Services.Dto;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(Locacion))]
    [Serializable]
    public class LocacionDto : EntityDto
    {
        public int Codigo { get; set; }
        public int ZonaId { get; set; }
        public ZonaDto Zona { get; set; }
        public string Nombre { get; set; }
        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
