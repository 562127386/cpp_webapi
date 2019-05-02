using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(ServicioProveedor))]
    [Serializable]
    public class ServicioProveedorDto : EntityDto
    {
        public int ServicioId { get; set; }

        public int ProveedorId { get; set; }

        public ServicioEstado Estado { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
