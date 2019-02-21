using Abp.Application.Services.Dto;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.cpp.calypso.seguridad.aplicacion
{
    [Serializable]
    //[AutoMapFrom(typeof(AuditoriaPropiedad))]
    [DisplayName("Gestión de Auditoria - Propiedades")]
    public class AuditoriaPropiedadDto : EntityDto
    {
        //
        // Resumen:
        //     Gets or sets the identifier of the audit entry property.
        public int AuditEntryPropertyID { get; set; }
        //
        // Resumen:
        //     Gets or sets the identifier of the audit entry.
        public int AuditEntryID { get; set; }
        ////
        //// Resumen:
        ////     Gets or sets the parent.
        //public AuditEntry Parent { get; set; }
        //
        // Resumen:
        //     Gets or sets the name of the property audited.
        [MaxLength(255)]
        public string PropertyName { get; set; }
        //
        // Resumen:
        //     Gets or sets the name of the relation audited.
        [MaxLength(255)]
        public string RelationName { get; set; }
        
        //
        // Resumen:
        //     Gets or sets a value indicating whether OldValue and NewValue is set.
        [NotMapped]
        public bool IsValueSet { get; set; }
        //
        // Resumen:
        //     Gets or sets the name of the property internally.
        [NotMapped]
        public string InternalPropertyName { get; set; }
        
        //
        // Resumen:
        //     Gets or sets the new value audited formatted.
        public string NewValueFormatted { get; set; }
        

        //
        // Resumen:
        //     Gets or sets the old value audited formatted.
        public string OldValueFormatted { get; set; }
    }

}
