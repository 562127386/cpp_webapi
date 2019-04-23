using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.dominio;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(ContratoProveedor))]
    [Serializable]
    public class ContratoProveedorDto : EntityDto
    {

        public int ProveedorId { get; set; }
        public ProveedorDto Proveedor { get; set; }
        public int EmpresaId { get; set; }
        public EmpresaDto Empresa { get; set; }
        public string Codigo { get; set; }
        public string Objeto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int PlazoPago { get; set; }
        public decimal Monto { get; set; }
        public string OrdenCompra { get; set; }
        public ContratoEstado Estado { get; set; }
        public int? DocumentacionId { get; set; }
        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
