using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using System.Collections.Generic;

namespace com.cpp.calypso.web
{
    public class UsuarioViewModel
    {
        /// <summary>
        /// Metadados para la paginacion
        /// </summary>
        public PagedListMetaDataModel Metadatos { get; set; }

        /// <summary>
        /// Listado 
        /// </summary>
        public IList<IUsuario> Usuarios { get; set; }
        
        /// <summary>
        /// Criteria para buscar
        /// </summary>
        public UsuarioCriteria Criteria { get; set; }
        

        public MensajeHelper Mensaje { get; set; }
    }

   

}
