using com.cpp.calypso.comun.dominio;
using System;
using System.ComponentModel;

namespace com.cpp.calypso.web
{
    public abstract class AuditableEntityViewModel : IViewModel
    {

        [DisplayNameAttribute("Fecha Creación")]
        public DateTime FechaCreacion { get; set; }

        [DisplayNameAttribute("Usuario Creación")]
        public string UsuarioCreacion { get; set; }


        [DisplayNameAttribute("Fecha Actualización")]
        public DateTime? FechaActualizacion { get; set; }


        [DisplayNameAttribute("Usuario Actualización")]
        public string UsuarioActualizacion { get; set; }
    }
}
