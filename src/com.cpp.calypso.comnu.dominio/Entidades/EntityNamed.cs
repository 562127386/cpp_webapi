using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace com.cpp.calypso.comun.dominio
{


    [Serializable]
    public abstract class EntityNamed : Entity, IEntityNamed
    {
        [Required(ErrorMessageResourceType = typeof(Mensajes), ErrorMessageResourceName = "RequiredAttribute_ValidationError")]
        [StringLength(15)]
        //[Index(IsUnique = true)]
        public string Codigo { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensajes), ErrorMessageResourceName = "RequiredAttribute_ValidationError")]
        [StringLength(80)]
        public string Nombre { get; set; }
    }


    [Serializable]
    public abstract class AuditedEntityEntityNamed : AuditedEntity, IEntityNamed
    {
        [Required(ErrorMessageResourceType = typeof(Mensajes), ErrorMessageResourceName = "RequiredAttribute_ValidationError")]
        [StringLength(15)]
        //[Index(IsUnique = true)]
        public string Codigo { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensajes), ErrorMessageResourceName = "RequiredAttribute_ValidationError")]
        [StringLength(80)]
        public string Nombre { get; set; }
    }

}
