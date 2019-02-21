using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;

namespace com.cpp.calypso.comun.entityframework
{

    /// <summary>
    /// Implmentacion base de repositorio en entity framework
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseRepository<TEntity> : EfRepositoryBase<PrincipalBaseDbContext, TEntity, int>, 
        IBaseRepository<TEntity>
        where TEntity : class, IEntity
    {
        public BaseRepository(IDbContextProvider<PrincipalBaseDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        /// <summary>
        /// Apply version if entry is versionable
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void ApplyVersionProperties(DbEntityEntry entry)
        {

            //TODO: Analizar como aplicar conceptos.. globales... ABP
            //TODO: JSA que pase si existe un error se debe reversar la version???
            IVersionable entityVersionable = entry.Entity as IVersionable;

            if (entityVersionable != null)
            {
                entityVersionable.VersionRegistro = entityVersionable.VersionRegistro + 1;
            }

        }

        public IPagedListMetaData<TEntity> GetList(int Skip, int Take)
        {
            ///TODO: JSA, como establecer la propiedad de la entidad que es ordenada por default ?? 
            return GetList(Skip, Take, "Id");
        }

        public IPagedListMetaData<TEntity> GetList(int Skip, int Take, string orderBy)
        {
            Guard.AgainstLessThanValue(Skip, "Skip", 0);
            Guard.AgainstLessThanValue(Take, "Take", 0);

            Guard.AgainstNullOrEmptyString(orderBy, "orderBy");

            var query = GetAll().AsQueryable();

            query = query.OrderBy(orderBy);


            var totalResultSetCount = query.Count();

            query = query.Skip(Skip).Take(Take);


            IEnumerable<TEntity> resultList;

            if (totalResultSetCount > 0)
                resultList = query.ToList();
            else
                resultList = new List<TEntity>();

            var result = new PagedListMetaData<TEntity>();
            result.TotalResultSetCount = totalResultSetCount;
            result.Subset = resultList.ToList();


            return result;
        }

        public IPagedListMetaData<TEntity> GetList(Expression<Func<TEntity, bool>> criteria, int Skip, int Take)
        {
            ///TODO: JSA, como establecer la propiedad de la entidad que es ordenada por default ?? 
            return GetList(criteria, Skip, Take, "Id");
        }

        public IPagedListMetaData<TEntity> GetList(Expression<Func<TEntity, bool>> criteria, int Skip, int Take, string orderBy)
        {
            Guard.AgainstLessThanValue(Skip, "Skip", 0);
            Guard.AgainstLessThanValue(Take, "Take", 0);


            var query = GetAll().AsQueryable();

            query = query.Where(criteria);

            query = query.OrderBy(orderBy);

            var totalResultSetCount = query.Count();

            query = query.Skip(Skip).Take(Take);


            IEnumerable<TEntity> resultList;

            if (totalResultSetCount > 0)
                resultList = query.ToList();
            else
                resultList = new List<TEntity>();

            var result = new PagedListMetaData<TEntity>();
            result.TotalResultSetCount = totalResultSetCount;
            result.Subset = resultList.ToList();


            return result;
        }

        public IPagedListMetaData<TEntity> GetList(IList<FilterEntity> filters, int Skip, int Take)
        {
            ///TODO: JSA, como establecer la propiedad de la entidad que es ordenada por default ?? 
            return GetList(filters, Skip, Take, "Id");
        }

        public IPagedListMetaData<TEntity> GetList(IList<FilterEntity> filters, int Skip, int Take, string orderBy)
        {
            Guard.AgainstLessThanValue(Skip, "Skip", 0);
            Guard.AgainstLessThanValue(Take, "Take", 0);


            var query = GetAll().AsQueryable();
 
            query = query.Where(filters);

            query = query.OrderBy(orderBy);

            var totalResultSetCount = query.Count();

            query = query.Skip(Skip).Take(Take);


            IEnumerable<TEntity> resultList;

            if (totalResultSetCount > 0)
                resultList = query.ToList();
            else
                resultList = new List<TEntity>();

            var result = new PagedListMetaData<TEntity>();
            result.TotalResultSetCount = totalResultSetCount;
            result.Subset = resultList.ToList();


            return result;
        }

        public IEnumerable<TEntity> GetList(IList<FilterEntity> filters, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            var query = GetAll().AsQueryable();
            query = query.Where(filters);

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public IEnumerable<TEntity> GetList(IList<FilterEntity> filters, string orderBy)
        {
            var query = GetAll().AsQueryable();
            query = query.Where(filters);

            return query.OrderBy(orderBy).ToList();
        }


        public IList<TEntity> SaveOrUpdate(IList<TEntity> listEntity)
        {
            Guard.AgainstArgumentNull(listEntity, "listEntity");

            var result = new List<TEntity>();

            foreach (var entity in listEntity)
            {

                var item = InsertOrUpdate(entity);
                result.Add(item);
            }


            return result; 

        }

        public void Delete(IList<TEntity> listEntity)
        {
            Guard.AgainstArgumentNull(listEntity, "listEntity");

            foreach (var entity in listEntity)
            {
                Delete(entity);
            }

             
        }

        public TEntity Get(int id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAll().AsQueryable();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            query = query.Where(c => c.Id == id);

            return query.SingleOrDefault();

        }
    }


}
