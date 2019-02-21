using com.cpp.calypso.comun.dominio;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace com.cpp.calypso.web
{
    public class ChangeMyPasswordViewModel : IViewModel
    {
        [Obligado]
        [DataType(DataType.Password)]
        [DisplayName("Contraseña Actual")]
        public string PasswordCurrent { get; set; }

        [Obligado]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [DisplayName("Nueva Contraseña")]
        public string Password { get; set; }


        [Obligado]
        [DataType(DataType.Password)]
        [DisplayName("Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La Nueva contraseña y contraseña de confirmación no coinciden.")]
        public string RepeatPassword { get; set; }
    }
}