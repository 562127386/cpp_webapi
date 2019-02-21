using System;
using System.ComponentModel.DataAnnotations;


namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Criteria para recuperar listado de registro de auditoria
    /// </summary>
    [Serializable]
    public class AuditoriaCriteria: ICriteria
    {
        [Display(ResourceType = typeof (Titulos), Name = "AuditoriaCriteria_Funcionalidad_DisplayName")]
        public string Funcionalidad { get; set; }

        [Display(ResourceType = typeof (Titulos), Name = "AuditoriaCriteria_Accion_DisplayName")]
        public string Accion { get; set; }

        [Display(ResourceType = typeof (Titulos), Name = "AuditoriaCriteria_Identificador_DisplayName")]
        public string Identificador { get; set; }


        [Display(ResourceType = typeof (Titulos), Name = "AuditoriaCriteria_Usuario_DisplayName")]
        public string Usuario { get; set; }

     
        /// <summary>
        /// Fecha de inicio del Regsitro
        /// </summary>
        [Fecha]
        [Display(ResourceType = typeof (Titulos), Name = "AuditoriaCriteria_FechaInicio_DisplayName")]
        public DateTime? FechaInicio { get; set; }

        /// <summary>
        /// Fecha de inicio del Final
        /// </summary>
        [Fecha]
        [Display(ResourceType = typeof (Titulos), Name = "AuditoriaCriteria_FechaFinal_DisplayName")]
        public DateTime? FechaFinal { get; set; }
    }
}
