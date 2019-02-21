using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace com.cpp.calypso.web
{
    public class RolDetalleViewModel : AuditableEntityViewModel
    {

        public int Id { get; set; }

         [DisplayNameAttribute("Código")]
        public string Codigo { get; set; }


        public string Nombre { get; set; }

        /// <summary>
        /// Si el rol es administrador
        /// </summary>
        [DisplayNameAttribute("Es Administrador")]
        public bool EsAdministrador { get; set; }

        /// <summary>
        /// Si el rol es externo (AD) o es interno Carpeta Linea
        /// </summary>
        [DisplayNameAttribute("Rol Externo")]
        public bool EsExterno { get; set; }


        [StringLength(255)]
        [DisplayNameAttribute("URL Inicio")]
        public string Url { get; set; }
    }
}