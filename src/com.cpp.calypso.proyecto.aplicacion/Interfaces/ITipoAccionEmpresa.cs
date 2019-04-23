using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.proyecto.dominio.Entidades;
using com.cpp.calypso.proyecto.aplicacion.Dto;

namespace com.cpp.calypso.proyecto.aplicacion.Interfaces
{
    public interface ITipoAccionEmpresaAsyncBaseCrudAppService : IAsyncBaseCrudAppService<TipoAccionEmpresa, TipoAccionEmpresaDto, PagedAndFilteredResultRequestDto>
    {
    }
}
