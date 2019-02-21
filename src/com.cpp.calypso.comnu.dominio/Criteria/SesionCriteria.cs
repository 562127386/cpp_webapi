using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// TODO: Pendiente, utilizar clase, para definir parametros de entrada para recuperar listados de session
    /// Criteria para recuperar listado de sesiones de acceso al sistema
    /// </summary>
    [Serializable]
    public class SesionCriteria : ICriteria
    {

        [LongitudMayor(50)]
        public string Cuenta { get; set; }


        public LoginResultType? Estado { get; set; }

        /// <summary>
        /// Fecha de inicio de Sesion 
        /// </summary>
        [DisplayNameAttribute("Fecha Inicio")]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Mensajes), ErrorMessageResourceName = "DataTypeAttribute_DateValidationError")]
        public DateTime? Fecha { get; set; }
    }
}
