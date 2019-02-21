using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;
using com.cpp.calypso.proyecto.dominio.Entidades;

namespace com.cpp.calypso.proyecto.aplicacion.Service
{
    public class ConsumoViandaAsyncBaseCrudAppService :
        AsyncBaseCrudAppService<ConsumoVianda, ConsumoViandaDto, PagedAndFilteredResultRequestDto>,
        IConsumoViandaAsyncBaseCrudAppService
    {
        public ConsumoViandaAsyncBaseCrudAppService(
            IBaseRepository<ConsumoVianda> repository
            ) : base(repository)
        {
        }
    }
}
