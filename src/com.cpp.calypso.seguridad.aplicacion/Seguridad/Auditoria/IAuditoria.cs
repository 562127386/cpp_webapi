using System;
using System.Collections.Generic;
using Z.EntityFramework.Plus;

namespace com.cpp.calypso.seguridad.aplicacion
{
    public interface IAuditoria
    {
        int AuditEntryID { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        object Entity { get; set; }
        string EntitySetName { get; set; }
        string EntityTypeName { get; set; }
        List<AuditoriaPropiedadDto> Properties { get; set; }
        AuditEntryState State { get; set; }
        string StateName { get; set; }
    }
}