using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.seguridad.aplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.cpp.calypso.web
{
    public class CrearUsuarioViewModel : IViewModel
    {
        public IReadOnlyList<RolDto> Roles { get; set; }

        public IReadOnlyList<ModuloDto> Modulos { get; set; }

        public CrearUsuarioDto Model { get; set; }

        public bool UsuarioTieneRol(RolDto role)
        {
            return Model.Roles != null && Model.Roles.Any(r => r == role.Nombre);
        }

        public bool UsuarioTieneModulo(ModuloDto dto)
        {
            return Model.Modulos != null && Model.Modulos.Any(r => r == dto.Id);
        }
    }
}