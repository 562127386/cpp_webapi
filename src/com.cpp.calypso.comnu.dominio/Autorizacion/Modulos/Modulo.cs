using Abp.Domain.Entities;
using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace com.cpp.calypso.comun.dominio
{
    [Serializable]
    public class Modulo : Entity
    {
        public Modulo()
        {
            Funcionalidades = new List<Funcionalidad>();
            Usuarios = new List<Usuario>();
        }

        [Obligado]
        [LongitudMayor(60)]
        public string Codigo { get; set; }

        [Obligado]
        [LongitudMayor(80)]
        public string Nombre { get; set; }


        [DisplayName("Descripción")]
        [LongitudMayor(255)]
        public string Descripcion { get; set; }

        /// <summary>
        /// Listado de funcionalidades del modulo
        /// </summary>
        [DisableValidation]
        public virtual ICollection<Funcionalidad> Funcionalidades { get; set; }

        [DisableValidation]
        public virtual ICollection<Usuario> Usuarios { get; set; }

        public void AddFuncionalidad(Funcionalidad funcionalidad)
        {
            funcionalidad.ModuloId = Id;
            funcionalidad.Modulo = this;
            Funcionalidades.Add(funcionalidad);
        }
 
    }
}
