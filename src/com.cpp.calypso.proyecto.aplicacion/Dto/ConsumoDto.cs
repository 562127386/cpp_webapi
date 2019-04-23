using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using com.cpp.calypso.comun.aplicacion;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(Consumo))]
    [Serializable]
    public class ConsumoDto : EntityDto
    {
        public int ProveedorId { get; set; }
        public ProveedorDto Proveedor { get; set; }
        public int ColaboradorId { get; set; }
        public ColaboradorDto Colaborador { get; set; }
        public DateTime Fecha { get; set; }
        public int TipoComidaId { get; set; }
        public CatalogoDto TipoComida { get; set; }
        public int OpcionComidaId { get; set; }
        public CatalogoDto OpcionComida { get; set; }
        public string Observacion { get; set; }
        public OrigenConsumo OrigenConsumoId { get; set; }
        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

    }
}
