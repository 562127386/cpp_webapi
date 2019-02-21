using Abp.Domain.Repositories;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.comun.dominio
{

    public interface IAuditoriaRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : Auditoria
    {

        /// <summary>
        /// Busca registro de auditoria. El resultado es paginado segun los parametros
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="Skip"></param>
        /// <param name="Take"></param>
        /// <returns></returns>
        IPagedListMetaData<TEntity> Buscar(AuditoriaCriteria criteria, int Skip, int Take);
    }
}
