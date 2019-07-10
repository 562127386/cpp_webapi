using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using com.cpp.calypso.comun.aplicacion;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(SincronizacionLog))]
    [Serializable]
    public class SincronizacionLogDto : EntityDto
    {
        public int UsuarioId { get; set; }

        public string Log { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
