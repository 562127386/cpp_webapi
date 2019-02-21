using System;
using Abp.Domain.Entities;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.comun.dominio
{
    [Serializable]
    public  class Permiso: Entity 
    {
        //public int Id { get; set; }

        [Obligado]
        public int RolId { get; set; }

        public virtual Rol Rol { get; set; }


        [Obligado]
        public int AccionId { get; set; }
 
        public virtual Accion Accion { get; set; }

     

    }
}
