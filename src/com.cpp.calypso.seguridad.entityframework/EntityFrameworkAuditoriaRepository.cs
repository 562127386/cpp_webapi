using Abp.EntityFramework;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.comun.entityframework;
using com.cpp.calypso.framework;
 
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace com.cpp.calypso.seguridad.entityframework
{
  
        public class EntityFrameworkAuditoriaRepository<TAuditoria> : BaseRepository<TAuditoria>, IAuditoriaRepository<TAuditoria>
            where TAuditoria: Auditoria
        {
        public EntityFrameworkAuditoriaRepository(IDbContextProvider<PrincipalBaseDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public IPagedListMetaData<TAuditoria> Buscar(AuditoriaCriteria criteria, int Skip, int Take)
        {
            Guard.AgainstLessThanValue(Skip, "Skip", 0);
            Guard.AgainstLessThanValue(Take, "Take", 0);


            var query =  GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(criteria.Identificador))
                query = query.Where(p => p.Identificacion.ToUpper().Trim().StartsWith(criteria.Identificador.ToUpper().Trim()));

            if (!string.IsNullOrEmpty(criteria.Usuario))
                query = query.Where(p => p.Usuario.ToUpper().Trim().StartsWith(criteria.Usuario.ToUpper().Trim()));

            if (!string.IsNullOrEmpty(criteria.Funcionalidad))
                query = query.Where(p => p.Funcionalidad.ToUpper().Trim().StartsWith(criteria.Funcionalidad.ToUpper().Trim()));

            if (!string.IsNullOrEmpty(criteria.Accion))
                query = query.Where(p => p.Accion.ToUpper().Trim().StartsWith(criteria.Accion.ToUpper().Trim()));


            //Fecha
            if (criteria.FechaInicio.HasValue && criteria.FechaFinal.HasValue)
            {
                query = query.Where(p => p.Fecha >= DbFunctions.TruncateTime(criteria.FechaInicio.Value) && p.Fecha <= DbFunctions.AddDays(criteria.FechaFinal.Value,1));
            }
            else {
                if (criteria.FechaInicio.HasValue) {
                    query = query.Where(p => p.Fecha >= DbFunctions.TruncateTime(criteria.FechaInicio.Value));
                }
                else if (criteria.FechaFinal.HasValue)
                {
                    query = query.Where(p => p.Fecha <= DbFunctions.AddDays(criteria.FechaFinal.Value, 1));
                }
            }

            query = query.OrderByDescending(p => p.Fecha);

            var totalResultSetCount = query.Count();

            query = query.Skip(Skip).Take(Take);


            IEnumerable<TAuditoria> resultList;

            if (totalResultSetCount > 0)
                resultList = query.ToList();
            else
                resultList = new List<TAuditoria>();

            var result = new PagedListMetaData<TAuditoria>();
            result.TotalResultSetCount = totalResultSetCount;
            result.Subset = resultList.ToList();


            return result;
        }
    }
}
