using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.dominio.Entidades;

namespace com.cpp.calypso.proyecto.aplicacion
{
    public interface ITipoOpcionComidaAsyncBaseCrudAppService : IAsyncBaseCrudAppService<TipoOpcionComida, TipoOpcionComidaDto, PagedAndFilteredResultRequestDto>
    {
    }
}
