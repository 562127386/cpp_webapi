using Abp.AutoMapper;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System;
using Abp.Application.Services.Dto;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(Proveedor))]
    [Serializable]
    public class ProveedorDto : EntityDto
    {

        public ProveedorTipoIdentificacion TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string RazonSocial { get; set; }
        public ProveedorEstado Estado { get; set; }
        public int EsExterno { get; set; }
        public string Coordenadas { get; set; }
        public string CodigoSap { get; set; }
        public int Usuario { get; set; }
        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

    }
}
