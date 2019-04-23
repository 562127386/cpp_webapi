using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.proyecto.aplicacion.Dto;
using com.cpp.calypso.proyecto.aplicacion.Interfaces;
using com.cpp.calypso.proyecto.dominio.Entidades;

namespace com.cpp.calypso.proyecto.aplicacion.Service
{
    public class SolicitudViandaAsyncBaseCrudAppService :
        AsyncBaseCrudAppService<SolicitudVianda, SolicitudViandaDto, PagedAndFilteredResultRequestDto>,
        ISolicitudViandaAsyncBaseCrudAppService
    {
        public SolicitudViandaAsyncBaseCrudAppService(IBaseRepository<SolicitudVianda> repository) : base(repository)
        {
        }
    }
}
