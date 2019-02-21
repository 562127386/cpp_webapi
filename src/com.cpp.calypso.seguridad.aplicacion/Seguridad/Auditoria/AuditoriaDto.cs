using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using Z.EntityFramework.Plus;

namespace com.cpp.calypso.seguridad.aplicacion
{


    [Serializable]
    //[AutoMapFrom(typeof(AuditoriaEntidad))]
    [DisplayName("Gestión de Auditoria")]
    public class AuditoriaDto : EntityDto
        //, IAuditoria
    {
       
        public int AuditEntryID { get; set; }
    
        public string CreatedBy { get; set; }
       
        public DateTime CreatedDate { get; set; }
      
        public object Entity { get; set; }
       
        public ObjectStateEntry Entry { get; set; }
    
        public string EntitySetName { get; set; }
      
        public string EntityTypeName { get; set; }
        
        //
        // Resumen:
        //     Gets or sets the properties.
        public List<AuditoriaPropiedadDto> Properties { get; set; }
      
        public AuditEntryState State { get; set; }
        //
        // Resumen:
        //     Gets or sets the name of the entry state.
        public string StateName { get; set; }

        //public override string ToString()
        //{
        //    return Nombre;
        //}

    }

}
