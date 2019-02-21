using System;
using Abp.Domain.Entities;


namespace com.cpp.calypso.comun.dominio
{

    /// <summary>
    /// Representa una Accion que se puede realizar en una funcionalidad (Editar, Crear, Eliminar, Visualizar, Imprimir, etc.)
    /// </summary>
    [Serializable]
    public class Accion : Entity, IEntityNamed
    {
        //public int Id { get; set; }

        /// <summary>
        /// Codigo de la accion. Esta accion se utiliza para mapear las acciones de los controladores en MVC
        /// </summary>
        [Obligado]
        [LongitudMayor(60)]
        public string Codigo { get; set; }

        [Obligado]
        [LongitudMayor(80)]
        public string Nombre { get; set; }

        [Obligado]
        public int FuncionalidadId { get; set; }

        [Obligado]
        public virtual Funcionalidad Funcionalidad { get; set; }
 

    }
}
