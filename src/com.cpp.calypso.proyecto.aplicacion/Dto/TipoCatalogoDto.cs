using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System;
using Abp.Application.Services.Dto;
using com.cpp.calypso.comun.aplicacion;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(TipoCatalogo))]
    [Serializable]
    public class TipoCatalogoDto : EntityDto
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string TipoOrdenamiento { get; set; }
        public bool Editable { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
