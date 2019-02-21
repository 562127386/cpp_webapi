using System.Collections.Generic;
using Abp.Domain.Entities;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;

namespace com.cpp.calypso.web
{
    /// <summary>
    /// Model View 
    /// </summary>
    public class TreeModelChildView : IViewModel
    {
        /// <summary>
        /// Titulo utilizado en formulario. Si no se especifica, se obtiene el titulo desde la
        /// descripcion del modelo hijo y datos del modelo padre
        /// </summary>
        public string Title;

        /// <summary>
        /// Identificador del Padre
        /// </summary>
        public int ParentId;

        /// <summary>
        /// Nombre del Padre
        /// </summary>
        public string ParentName;

        /// <summary>
        /// Model Hijo
        /// </summary>
        public IEnumerable<IEntity> ChildModel;

        /// <summary>
        /// View para listados
        /// </summary>
        public Tree ChildView;

        /// <summary>
        /// Vista de busqueda asociada
        /// </summary>
        public View SearchChildView;

        /// <summary>
        /// Mensaje que se visualizan. Ejemplo: Resultado de alguna accion anterior ejecutada, creacion de entidad
        /// </summary>
        public MensajeHelper Mensaje { get; set; }


        /// <summary>
        ///  Paged List Meta Data
        /// </summary>
        public PagedListMetaDataModel PagedListMetaData { get; set; }
 

    }

}