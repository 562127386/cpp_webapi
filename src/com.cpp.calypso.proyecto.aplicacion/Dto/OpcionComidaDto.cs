using Abp.AutoMapper;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System;
using Abp.Application.Services.Dto;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(OpcionComida))]
    [Serializable]
    public class OpcionComidaDto : EntityDto
    {
        public int Opcion { get; set; }
        public string Nombre { get; set; }
        public decimal Costo { get; set; }
        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }

    }
}
