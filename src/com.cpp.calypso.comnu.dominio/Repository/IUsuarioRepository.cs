using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Repositorio para el usuario
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IUsuarioRepository<TEntity>: IBaseRepository<TEntity>
        where TEntity : Usuario
    {
  
        /// <summary>
        /// Obtener el usuario por su cuenta
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        TEntity Get(string cuenta);
  

        ///// <summary>
        ///// Busca registro de usuario. El resultado es paginado segun los parametros
        ///// </summary>
        ///// <param name="criteria"></param>
        ///// <param name="Skip"></param>
        ///// <param name="Take"></param>
        ///// <returns></returns>
        //IPagedListMetaData<TEntity> Buscar(UsuarioCriteria criteria, int Skip, int Take);

        
    }
}
