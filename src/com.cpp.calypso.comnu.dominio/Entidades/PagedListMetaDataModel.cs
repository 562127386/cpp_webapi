using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Metadatos para la paginacion
    /// </summary>
    public class PagedListMetaDataModel : PagedListMetaData, IViewModel
    {

        public PagedListMetaDataModel()
        {

            TotalItemCount = 0;
            PageSize = 0;
            PageNumber = 0;
            PageCount = 0;
            HasPreviousPage = false;
            HasNextPage = false;
            IsFirstPage = false;
            IsLastPage = false;
            FirstItemOnPage = 0;
            LastItemOnPage = 0;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber">Numero de Pagina actual</param>
        /// <param name="pageSize">Tamaño de la pagina</param>
        /// <param name="totalItemCount">Total de itemas a paginar</param>
        public PagedListMetaDataModel(int pageNumber, int pageSize, int totalItemCount)
        {
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "PageNumber cannot be below 1.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "PageSize cannot be less than 1.");

            // set source to blank list if superset is null to prevent exceptions
            TotalItemCount = totalItemCount;
            PageSize = pageSize;
            PageNumber = pageNumber;
            PageCount = TotalItemCount > 0
                            ? (int)Math.Ceiling(TotalItemCount / (double)PageSize)
                            : 0;
            HasPreviousPage = PageNumber > 1;
            HasNextPage = PageNumber < PageCount;
            IsFirstPage = PageNumber == 1;
            IsLastPage = PageNumber >= PageCount;
            FirstItemOnPage = (PageNumber - 1) * PageSize + 1;
            var numberOfLastItemOnPage = FirstItemOnPage + PageSize - 1;
            LastItemOnPage = numberOfLastItemOnPage > TotalItemCount
                                ? TotalItemCount
                                : numberOfLastItemOnPage;
        }

    }
}
