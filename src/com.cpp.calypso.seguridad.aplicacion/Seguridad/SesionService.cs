using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.seguridad.aplicacion
{
    public class SesionService: GenericService<Sesion>, ISesionService
    {

        private ISesionRepository<Sesion> _repositorySesion;

        public SesionService(
                  ISesionRepository<Sesion> repository):base(repository)
        {
            _repositorySesion = repository;
        }


        public IPagedListMetaData<Sesion> Buscar(SesionCriteria criteria, int Skip, int Take) {

            return _repositorySesion.Buscar(criteria, Skip, Take);
        }


    }
}
