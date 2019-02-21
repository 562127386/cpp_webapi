using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.comun.dominio
{
    public interface IMenuRespository
    {

        /// <summary>
        /// Obtener un listado de items de menus de un menu especifico, asociado a un ROL. (Mecanismo de seguridad)
        /// </summary>
        /// <param name="codigoMenu"></param>
        /// <param name="rolId"></param>
        /// <returns></returns>
        IList<MenuItem> GetMenuItemAssociatedRole(string codigoMenu,int rolId);

    }
}
