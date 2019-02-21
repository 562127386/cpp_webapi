using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.web
{
    /// <summary>
    /// Model para el login del usuario
    /// </summary>
    public class IngresoViewModel : IViewModel
    {
        [Required]
        [DisplayName("Usuario")]
        [StringLength(60, ErrorMessage = "El {0} debe tener mínimo {2} caracteres y máximo {1} caracteres de longitud", MinimumLength = 2)]
        public string Usuario { get; set; }

        [Obligado]
        [DataType(DataType.Password)]
        [DisplayName("Contraseña")]
        public string Password { get; set; }
    
        public IngresoViewModel()
        {
            
     
        }

    }

    

    
}