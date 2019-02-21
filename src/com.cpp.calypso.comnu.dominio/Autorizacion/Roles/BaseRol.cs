using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseRol : 
        AuditedEntityEntityNamed, IEquatable<BaseRol> {

 
        /// <summary>
        /// Si el rol es administrador
        /// </summary>
        [DisplayName("Es Administrador")]
        public virtual bool EsAdministrador { get; set; }

        /// <summary>
        /// Si el rol es externo (AD) o interno (Sistema)
        /// </summary>
        [DisplayName("Rol Externo")]
        public virtual bool EsExterno { get; set; }

        [LongitudMayor(255)]
        [DisplayName("URL Inicio")]
        public virtual string Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Parametros { get; set; }


        /// <summary>
        /// Listado de usuarios
        /// </summary>
        [DisableValidation]
        public virtual ICollection<Usuario> Usuarios { get; set; }

        /// <summary>
        /// Listado de acciones/funcionalidades que tiene permisos el rol
        /// </summary>
        [DisableValidation]
        public virtual ICollection<Permiso> Permisos { get; set; }


        //public  int VersionRegistro
        //{
        //    get;
        //    set;
        //}

        public bool Equals(BaseRol other)
        {
            if (other == null) return false;
            return (this.Codigo.Equals(other.Codigo));
        }

        public override string ToString()
        {
            return string.Format("{0}", Nombre);
        }
    }
        

}
