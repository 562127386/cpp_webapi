using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Clase para almacenar una lista paginada
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedListMetaData<T>
    {
        /// <summary>
        /// Listado de items paginados
        /// </summary>
        IList<T> Subset { get; set; }

        /// <summary>
        /// Total de la lista, que se esta paginando
        /// </summary>
        int TotalResultSetCount { get; set; }
    }
}
