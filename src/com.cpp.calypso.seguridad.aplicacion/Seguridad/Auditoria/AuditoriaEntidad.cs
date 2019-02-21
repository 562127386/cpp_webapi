using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Z.EntityFramework.Plus;

namespace com.cpp.calypso.seguridad.aplicacion
{
    public class AuditoriaEntidad : AuditEntry, IEntity
    {
        private int _Id;

        //[NotMapped]
        public int Id {
            get { return this.AuditEntryID; }
            set { _Id = value;  } 
        }

        //public int Id
        //{
        //    get { return this.AuditEntryID; }

        //    set
        //    {
        //        AuditEntryID = value;
        //    }
        //}

        /// <summary>
        /// Checks if this entity is transient (it has not an Id).
        /// </summary>
        /// <returns>True, if this entity is transient</returns>
        public virtual bool IsTransient()
        {
            if (EqualityComparer<int>.Default.Equals(Id, default(int)))
            {
                return true;
            }

            //Workaround for EF Core since it sets int/long to min value when attaching to dbcontext
            return Convert.ToInt32(Id) <= 0;
        }
    }

}
