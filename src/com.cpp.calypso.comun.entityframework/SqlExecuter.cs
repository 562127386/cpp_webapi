using Abp.Dependency;
using Abp.EntityFramework;
using com.cpp.calypso.comun.dominio;
using System.Threading.Tasks;

namespace com.cpp.calypso.comun.entityframework
{
    /// <summary>
    /// Ejecutar SQL
    /// </summary>
    public class SqlExecuter : ISqlExecuter, ITransientDependency
    {
        private readonly IDbContextProvider<PrincipalBaseDbContext> DbContextProvider;

        public SqlExecuter(IDbContextProvider<PrincipalBaseDbContext> dbContextProvider )
        {
            DbContextProvider = dbContextProvider;
             
        }

        public int Execute(string sql, params object[] parameters)
        {
            return DbContextProvider.GetDbContext().Database.ExecuteSqlCommand(sql, parameters);
        }

        public async Task<T> SqlQuery<T>(string sql, params object[] parameters) {

            var value = await DbContextProvider.GetDbContext().Database.SqlQuery<T>(sql, parameters).FirstOrDefaultAsync();

            return value;
        }
    }
}
