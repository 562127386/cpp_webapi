using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.proyecto.dominio.Entidades;
using System;

namespace com.cpp.calypso.proyecto.aplicacion.Dto
{
    [AutoMap(typeof(UsuarioMovil))]
    [Serializable]
    public class UsuarioMovilDto : EntityDto
    {
        public int UsuarioId { get; set; }

        public string Username { get; set; }

        public string ApellidosNombres { get; set; }

        public string Pin { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? UltimoAcceso { get; set; }

        public string Rol { get; set; }

        public bool ActivoMovil { get; set; }

        public int Version { get; set; }

        public string Ref { get; set; }

        public bool IsDeleted { get; set; }
    }
}
