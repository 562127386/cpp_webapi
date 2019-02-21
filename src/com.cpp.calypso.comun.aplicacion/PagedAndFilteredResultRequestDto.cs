using Abp.Application.Services.Dto;
using com.cpp.calypso.comun.dominio;
using System.Collections.Generic;

namespace com.cpp.calypso.comun.aplicacion
{
    public class PagedAndFilteredResultRequestDto : PagedAndSortedResultRequestDto, IPagedAndFilteredResultRequestDto
    {
       public virtual IList<FilterEntity> Filter { get; set; }
    }

     


}
