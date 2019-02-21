using Abp.Application.Services.Dto;
using com.cpp.calypso.comun.dominio;
using System.Collections.Generic;

namespace com.cpp.calypso.web
{
    /// <summary>
    /// Un model view, para entidades dto. 
    /// </summary>
    public class EntityDtoViewModel : IViewModel
    {
        /// <summary>
        /// Listado de metadatos, para generar el formulario. 
        /// Tkey: nombre de la propiedad
        /// TValue: objeto utilizado para la propiedad
        /// Ejemplo:
        /// Listado de items, para establecer un valor de un campo.
        /// Listado de enum, para establecer un valor de un campo.
        /// </summary>
        public Dictionary<string, object> Metadata;

        public IEntityDto ModelDto { get; internal set; }
    }
}