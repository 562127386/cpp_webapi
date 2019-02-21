using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using com.cpp.calypso.comun.dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace com.cpp.calypso.seguridad.aplicacion
{

    [Serializable]
    [AutoMap(typeof(Rol))]
    [DisplayName("Gestión de Roles")]
    public class RolPermisosDto: RolDto
    {

        public virtual ICollection<PermisoDto> Permisos { get; set; }
    }

    [Serializable]
    [AutoMap(typeof(Permiso))]
    [DisplayName("Permisos")]
    public class PermisoDto: EntityDto
    {
       
        //public int Id { get; set; }

        [Obligado]
        public int RolId { get; set; }

        public virtual Rol Rol { get; set; }
 
        [Obligado]
        public int AccionId { get; set; }

        public virtual AccionDto Accion { get; set; }
 
    }

    [Serializable]
    [AutoMap(typeof(Accion))]
    public class AccionDto: EntityDto
    {

        /// <summary>
        /// Codigo de la accion. Esta accion se utiliza para mapear las acciones de los controladores en MVC
        /// </summary>
        [Obligado]
        [LongitudMayor(60)]
        public string Codigo { get; set; }

        [Obligado]
        [LongitudMayor(80)]
        public string Nombre { get; set; }

        [Obligado]
        public int FuncionalidadId { get; set; }
    }
}