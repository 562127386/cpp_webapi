using Abp.AutoMapper;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System;
using Abp.Application.Services.Dto;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(Empresa))]
    [Serializable]
    public class EmpresaDto : EntityDto
    {
        public TipoDeIdentificacion TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public bool Estado { get; set; }
        public string Telefono { get; set; }
        public TipoDeSociedad TipoSociedad { get; set; }
        public string Observaciones { get; set; }
        public bool EsPrincipal { get; set; }
        public TipoDeContribuyente TipoContribuyente { get; set; }
        public string CodigoSap { get; set; }
        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
