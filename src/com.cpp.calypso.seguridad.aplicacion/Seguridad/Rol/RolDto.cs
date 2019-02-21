using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.comun.dominio;
using System;
using System.ComponentModel;

namespace com.cpp.calypso.seguridad.aplicacion
{

    [Serializable]
    [AutoMap(typeof(Rol))]
    [AutoMapFrom(typeof(RolPermisosDto))]
    [DisplayName("Roles")]
    public class RolDto : EntityDto
    {
       [Obligado]
        [LongitudMayor(15)]
        public string Codigo { get; set; }

        [Obligado]
        [LongitudMayor(80)]
        public string Nombre { get; set; }

        /// <summary>
        /// Si el rol es administrador
        /// </summary>
        [DisplayName("Es Administrador")]
        public bool EsAdministrador { get; set; }

        /// <summary>
        /// Si el rol es externo (AD) o es interno Carpeta Linea
        /// </summary>
        [DisplayName("Rol Externo")]
        public bool EsExterno { get; set; }

        [LongitudMayor(255)]
        [DisplayName("URL Inicio")]
        public string Url { get; set; }

        public override string ToString()
        {
            return Nombre; 
        }
    }
}