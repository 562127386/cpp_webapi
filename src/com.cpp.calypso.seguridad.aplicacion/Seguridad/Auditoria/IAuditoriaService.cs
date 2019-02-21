using com.cpp.calypso.comun.aplicacion;

namespace com.cpp.calypso.seguridad.aplicacion
{
    public interface IAuditoriaService : 
        IAsyncBaseCrudAppService<AuditoriaEntidad, AuditoriaDto, PagedAndFilteredResultRequestDto>
    {

    }
}