using Abp.Domain.Repositories;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.comun.dominio
{

    public interface ISesionRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : Sesion
    {

        /// <summary>
        /// Busca sesiones. El resultado es paginado segun los parametros
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="Skip"></param>
        /// <param name="Take"></param>
        /// <returns></returns>
        IPagedListMetaData<TEntity> Buscar(SesionCriteria criteria, int Skip, int Take);
    }
}
