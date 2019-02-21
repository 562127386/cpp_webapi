using Abp.Domain.Entities.Auditing;
using com.cpp.calypso.comun.dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace com.cpp.calypso.comun.dominio
{

    /// <summary>
    /// Representa un Rol de Seguridad
    /// </summary>
    [Serializable]
    [DisplayName("Gestión de Roles")]
    public class Rol : AspRole<Usuario>
    {

        public Rol()
        {
            Usuarios = new List<Usuario>();
            Permisos = new List<Permiso>();
        }



        /// <summary>
        /// Reglas de Negocio
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> CanRemoved()
        {
            //Si es de tipo administrador, no se permite eliminar el rol

            if (EsAdministrador)
            {
                return new Tuple<bool, string>(false, string.Format("El rol [{0}], es administador", Nombre));
            }

            return new Tuple<bool, string>(true,"ok");
        }

    }
        

}
