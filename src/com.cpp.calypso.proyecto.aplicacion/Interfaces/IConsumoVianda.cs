using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.dominio.Entidades;

namespace com.cpp.calypso.proyecto.aplicacion.Interfaces
{
    public interface IConsumoViandaAsyncBaseCrudAppService : IAsyncBaseCrudAppService<ConsumoVianda, ConsumoViandaDto, PagedAndFilteredResultRequestDto>
    {
    }
}
