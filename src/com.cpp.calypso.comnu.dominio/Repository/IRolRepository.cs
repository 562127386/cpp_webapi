using Abp.Domain.Repositories;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Repositorio para el rol
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRolRepository<TEntity>: IBaseRepository<TEntity>
        where TEntity : Rol
    {
   
        
    }
}
