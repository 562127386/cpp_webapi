using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using com.cpp.calypso.comun.aplicacion;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(TipoOpcionComida))]
    [Serializable]
    public class TipoOpcionComidaDto : EntityDto
    {
        public int ContratoId { get; set; }
        public ContratoProveedorDto Contrato { get; set; }
        public int OpcionComidaId { get; set; }
        public CatalogoDto OpcionComida { get; set; }
        public decimal Costo { get; set; }
        public int TipoComidaId { get; set; }
        public CatalogoDto TipoComida { get; set; }
        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
