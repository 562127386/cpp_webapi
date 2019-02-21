using Abp.EntityFramework;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.comun.entityframework;
using com.cpp.calypso.framework;
 
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace com.cpp.calypso.seguridad.entityframework
{
    public class EntityFrameworkSesionRepository<TSesion> : BaseRepository<TSesion>, ISesionRepository<TSesion>
        where TSesion : Sesion
    {
        public EntityFrameworkSesionRepository(IDbContextProvider<PrincipalBaseDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }


        //public EntityFrameworkSesionRepository(DbContext context, IIdentityUser identityUser, IManagerDateTime managerDateTime)
        //    : base(context, identityUser, managerDateTime)
        //{

        //}

        public IPagedListMetaData<TSesion> Buscar(SesionCriteria criteria, int Skip, int Take)
        {
            Guard.AgainstLessThanValue(Skip, "Skip", 0);
            Guard.AgainstLessThanValue(Take, "Take", 0);


            var query =  GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(criteria.Cuenta))
                query = query.Where(p => p.Cuenta.ToUpper().Trim().StartsWith(criteria.Cuenta.ToUpper().Trim()));

            if (criteria.Estado.HasValue) {
                query = query.Where(p => p.Result == criteria.Estado.Value);
            }

            if (criteria.Fecha.HasValue)
            {
                query = query.Where(p => p.CreationTime >= DbFunctions.TruncateTime(criteria.Fecha.Value) && p.CreationTime <= DbFunctions.AddDays(criteria.Fecha.Value,1));

            }

            query = query.OrderByDescending(p => p.CreationTime);

            var totalResultSetCount = query.Count();

            query = query.Skip(Skip).Take(Take);


            IEnumerable<TSesion> resultList;

            if (totalResultSetCount > 0)
                resultList = query.ToList();
            else
                resultList = new List<TSesion>();

            var result = new PagedListMetaData<TSesion>();
            result.TotalResultSetCount = totalResultSetCount;
            result.Subset = resultList.ToList();


            return result;
        }
    }
}
