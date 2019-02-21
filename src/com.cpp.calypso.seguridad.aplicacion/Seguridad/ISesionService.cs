using Abp.Application.Services;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.seguridad.aplicacion
{
    public interface ISesionService: IEntityService<Sesion>
    {
        IPagedListMetaData<Sesion> Buscar(SesionCriteria criteria, int Skip, int Take);
    }
}