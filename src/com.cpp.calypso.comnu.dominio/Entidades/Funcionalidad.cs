using System;
using System.Collections.Generic;
using System.ComponentModel;
using Abp.Domain.Entities;
using Abp.Runtime.Validation;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Representa una Funcionalidad de un sistema
    /// </summary>
    [Serializable]
    public class Funcionalidad : Entity 
    {
        public Funcionalidad()
        {
            Acciones = new List<Accion>(); 
        }


        //public int Id { get; set; }

        [Obligado]
        [LongitudMayor(15)]
        public  string Codigo { get; set; }

        [Obligado]
        [LongitudMayor(80)]
        public  string Nombre { get; set; }

        [LongitudMayor(255)]
        [DisplayNameAttribute("Descripción")]
        public  string Descripcion { get; set; }


        /// <summary>
        /// Nombre del controlador que gestiona la funcionalidad. (MVC. Nombre del Controller)
        /// </summary>
        [Obligado]
        public  string Controlador { get; set; }


        [Obligado]
        public EstadoFuncionalidad Estado { get; set; }


        /// <summary>
        /// Listado de acciones que se permite realizar sobre esta funcionalidad
        /// </summary>
        [DisableValidation]
        public virtual ICollection<Accion> Acciones { get; set; }

        public void AddAccion(Accion accion)
        {
            accion.FuncionalidadId = Id;
            accion.Funcionalidad = this;
            Acciones.Add(accion);
        }

        [Obligado]
        public virtual int ModuloId { get; set; }

        public virtual Modulo Modulo { get; set; }
 
    }


    public enum EstadoFuncionalidad
    {
        Desactiva = 0,
        Activa = 1
    }


}
