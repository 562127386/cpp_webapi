using System.Collections.Generic;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.web
{

    public class AuditoriaViewModel : IViewModel
    {

        /// <summary>
        /// Metatados para la paginacion
        /// </summary>
        public PagedListMetaDataModel Metadatos { get; set; }

        /// <summary>
        /// Listado 
        /// </summary>
        public IList<Auditoria> Auditorias { get; set; }
 

        /// <summary>
        /// Criteria para buscar
        /// </summary>
        public AuditoriaCriteria Criteria { get; set; }


        

    }
}
