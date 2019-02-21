using System.Data.Entity;
using System;
using System.Linq.Expressions;
using System.Linq;
using Abp.EntityFramework;
using com.cpp.calypso.comun.entityframework;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.seguridad.entityframework
{

    public class EntityFrameworkRolRepository<TRol> : BaseRepository<TRol>, IRolRepository<TRol> 
        where TRol : Rol
      {
        public EntityFrameworkRolRepository(IDbContextProvider<PrincipalBaseDbContext> dbContextProvider) :
            base(dbContextProvider)
        {
        }

    

        public override IQueryable<TRol> GetAllIncluding(params Expression<Func<TRol, object>>[] propertySelectors)
        {
            var query = GetAll(); //.AsNoTracking();
            foreach (var includeProperty in propertySelectors)
            {
                query = query.Include(includeProperty);
            }
            
            return query;
        }

         


    }
}
