using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using com.cpp.calypso.comun.aplicacion;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(RespaldoMovil))]
    [Serializable]
    public class RespaldoMovilDto : EntityDto
    {
        public int UserId { get; set; }

        public string Json { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string Estado { get; set; }
    }
}
