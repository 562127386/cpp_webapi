using System.Collections.Generic;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.web
{
    /// <summary>
    /// 
    /// </summary>
    public class SeleccionarRolViewModel : IViewModel
    {
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }

        public string IPClient { get; set; }

        public List<RolViewModel> Roles { get; set; }

        public SeleccionarRolViewModel()
        {
            Roles = new List<RolViewModel>();
        }

    }
}
