using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using System.ComponentModel.DataAnnotations;

namespace com.cpp.calypso.web
{
    public class EnrolamientoUsuarioViewModel
    {
        /// <summary>
        /// Usuario
        /// </summary>
        public Usuario Usuario { get; set; }

        /// <summary>
        /// Listado 
        /// </summary>
        [Obligado]
        [Display(Name = "Roles")]
        public int[] Roles { get; set; }

        public string RolesNombre { get; set; }

        /// <summary>
        /// Mensaje
        /// </summary>
        public MensajeHelper Mensaje { get; set; }

        public UsuarioCriteria Criteria { get; set; }

    }
}
