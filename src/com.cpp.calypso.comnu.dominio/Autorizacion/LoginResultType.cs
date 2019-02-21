using System.ComponentModel.DataAnnotations;

namespace com.cpp.calypso.comun.dominio
{
    public enum LoginResultType : byte
    {
        /// <summary>
        /// Si el usuario se encuentra correctamente autentificada. (Usuario / Modulo)
        /// </summary>
        [Display(Name = "Acceso Existoso")]
        Success = 1,

        /// <summary>
        /// Si el usuario / password es correcto. 
        /// </summary>
        [Display(Name = "Autentificación Existosa")]
        SucessAuthentication = 2,

        [Display(Name = "Autentificación Existosa, requiere cambio de clave")]
        SucessPasswordResetCode = 3,

        /// <summary>
        /// Usuario no existe o es invalido
        /// </summary>
        [Display(Name = "Usuario invalido")]
        InvalidUserNameOrEmailAddress,

        /// <summary>
        /// Clave es invalida
        /// </summary>
        [Display(Name = "Clave invalida")]
        InvalidPassword,

        /// <summary>
        /// Uusuario se encuentra bloqueado
        /// </summary>
        [Display(Name = "Usuario bloqueado")]
        LockedOut
        
    }
}
