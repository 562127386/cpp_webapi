using com.cpp.calypso.comun.dominio;
using System.ComponentModel;

namespace com.cpp.calypso.web
{
    public class RecuperarClaveViewModel
    {

        [Obligado]
        [DisplayNameAttribute("correo electrónico o el nombre de tu cuenta")]
        public string CorreoElectronicoCuenta { get; set; }


    }
}