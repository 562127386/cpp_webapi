using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using com.cpp.calypso.comun.aplicacion;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(ConsumoQr))]
    [Serializable]
    public class ConsumoQrDto : EntityDto
    {
        public int ProveedorId { get; set; }
        public ProveedorDto Proveedor { get; set; }
        public int TipoComidaId { get; set; }
        public CatalogoDto TipoComida { get; set; }
        public int OpcionComidaId { get; set; }
        public CatalogoDto OpcionComida { get; set; }
        public string Cedula { get; set; }
        public DateTime FechaConsumo { get; set; }
        public int UsuarioGeneradorId { get; set; }
        public OrigenConsumo OrigenConsumoId { get; set; }
        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
