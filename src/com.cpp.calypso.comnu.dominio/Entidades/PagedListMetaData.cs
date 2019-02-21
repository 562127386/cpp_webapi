using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Representa los metadatos para listas paginas. Guarda la información del total de items existentes en la lista, y a lista pagina (subset)
    /// </summary>
    [Serializable]
    public class PagedListMetaData<T> : IPagedListMetaData<T>
    {
        /// <summary>
        /// Total de items que existe en la lista sin paginacion
        /// </summary>
        public int TotalResultSetCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<T> Subset { get; set; }
    }
}
