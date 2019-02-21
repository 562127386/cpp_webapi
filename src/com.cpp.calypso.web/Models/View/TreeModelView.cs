using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;

namespace com.cpp.calypso.web
{
    /// <summary>
    /// Model View 
    /// </summary>
    public class TreeModelView : IViewModel
    {
        /// <summary>
        /// Titulo utilizado en formulario. Si no se especifica, se obtiene el titulo desde la
        /// descripcion del modelo
        /// </summary>
        public string Title;


        /// <summary>
        /// Model
        /// </summary>
        public IEnumerable<IEntity> Model;


        /// <summary>
        /// Model (DTO)
        /// </summary>
        public IEnumerable<IEntityDto> ModelDto;

        /// <summary>
        /// The string value should comma separated list of fields to sort 
        /// For example: "foo,bar". 
        /// 
        /// The default sorting order is ascending. 
        /// 
        /// To specify descending order for a field, a suffix " desc" should be appended to the field name. 
        /// For example: "foo desc,bar".
        /// </summary>
        public string OrderBy;


        /// <summary>
        /// View
        /// </summary>
        public Tree View;

        /// <summary>
        /// 
        /// </summary>
        public View SearchView;

        /// <summary>
        /// Mensaje que se visualizan. Ejemplo: Resultado de alguna accion anterior ejecutada, creacion de entidad
        /// </summary>
        public MensajeHelper Mensaje { get; set; }


        /// <summary>
        ///  Paged List Meta Data
        /// </summary>
        public PagedListMetaDataModel PagedListMetaData { get; set; }

        /// <summary>
        /// Criteria
        /// </summary>
        //public ICriteria Criteria { get; set; }

    }

}