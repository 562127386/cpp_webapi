using Abp.AutoMapper;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System;
using Abp.Application.Services.Dto;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(TipoAccionEmpresa))]
    [Serializable]
    public class TipoAccionEmpresaDto : EntityDto
    {
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public int TipoComidaId { get; set; }
        public Catalogo TipoComida { get; set; }
        public int AccionId { get; set; }
        public Catalogo Accion { get; set; }
        public DateTime HoraDesde { get; set; }
        public DateTime HoraHasta { get; set; }
        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

    }
}
