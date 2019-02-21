using Abp.Domain.Entities;
using Abp.EntityFramework;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Data.Entity;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.comun.entityframework
{
    public class BaseRepositoryNamed<TEntityNamed> : BaseRepository<TEntityNamed>, IRepositoryNamed<TEntityNamed>
       where TEntityNamed : class, IEntityNamed, IEntity
    {
        public BaseRepositoryNamed(IDbContextProvider<PrincipalBaseDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public TEntityNamed Get(string codigo)
        {
            return GetAll().Where(c => c.Codigo == codigo).SingleOrDefault();
        }

        public TEntityNamed Get(string codigo, params Expression<Func<TEntityNamed, object>>[] includeProperties)
        {
            var query = GetAll().AsQueryable();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            query = query.Where(c => c.Codigo == codigo);

            return query.SingleOrDefault();
        }
    }


}
