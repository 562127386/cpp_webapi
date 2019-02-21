using System.Collections.Generic;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.seguridad.aplicacion;

namespace com.cpp.calypso.web
{
    public class PermisoViewModel : IViewModel
    {
        public RolPermisosDto Rol { get; set; }

        public IEnumerable<Funcionalidad> Funcionalidades { get; set; }
 

    }
}