using System;
using System.Collections.Generic;
using System.Linq.Expressions;
 
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using System.Linq;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.comun.aplicacion
{

    /// <summary>
    /// Interface para un Servicio
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    public interface IEntityService<Entity> : IService where Entity : IEntity
    {
        /// <summary>
        /// Get Entity for key
        /// </summary>
        /// <param name="id">key</param>
        /// <returns></returns>
        Entity Get(int id);

        /// <summary>
        /// Get List Entity
        /// </summary>
        /// <returns></returns>
        IEnumerable<Entity> GetList();

        /// <summary>
        /// Get List Entity, with filters
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        IEnumerable<Entity> GetList(IList<FilterEntity> filters);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        IEnumerable<Entity> GetList(IList<FilterEntity> filters, string orderBy);

        /// <summary>
        /// Get List Entity, with predicate
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IEnumerable<Entity> GetList(Expression<Func<Entity, bool>> criteria);


        /// <summary>
        /// Get List entity, with predicate  and pagination
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IPagedListMetaData<Entity> GetList(Expression<Func<Entity, bool>> criteria, int skip, int take);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="Skip"></param>
        /// <param name="Take"></param>
        /// <returns></returns>
        IPagedListMetaData<Entity> GetList(IList<FilterEntity> filters, int Skip, int Take);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="Skip"></param>
        /// <param name="Take"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        IPagedListMetaData<Entity> GetList(IList<FilterEntity> filters, int Skip, int Take, string orderBy);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Skip"></param>
        /// <param name="Take"></param>
        /// <returns></returns>
        IPagedListMetaData<Entity> GetList(int Skip, int Take);

        /// <summary>
        /// Save or Update Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Entity SaveOrUpdate(Entity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listEntity"></param>
        /// <returns></returns>
        IList<Entity> SaveOrUpdate(IList<Entity> listEntity);

        /// <summary>
        /// Delete Entity for key
        /// </summary>
        /// <param name="id"></param>
        void Eliminar(int id);

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="entity"></param>
        void Eliminar(Entity entity);


        /// <summary>
        /// Gets count of all entities in this repository (use if expected return value is greather than <see cref="int.MaxValue"/>.
        /// </summary>
        /// <returns>Count of entities</returns>
        long LongCount();


        /// <summary>
        /// Gets count of all entities in this repository based on given <paramref name="predicate"/>
        /// (use this overload if expected return value is greather than <see cref="int.MaxValue"/>).
        /// </summary>
        /// <param name="predicate">A method to filter count</param>
        /// <returns>Count of entities</returns>
        long LongCount(Expression<Func<Entity, bool>> predicate);
    }

    /// <summary>
    /// TODO: Crear configuracion para conocer si se aplica cache o no ???
    /// TODO: Tiene alguna ventaja que la clase base/generica sea abstract ??
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    /// 
    [Obsolete("Usar AsyncBaseCrudAppService, para trabajar con DTO. Utilizar unicamente con entidades simples que no tengan dependencias complejas")]
    public class GenericService<Entity> : IEntityService<Entity> where Entity : class, IEntity
    {
        protected IBaseRepository<Entity> _repository;

        public GenericService(
           IBaseRepository<Entity> repository)
        {
            _repository = repository;
        }

        public virtual void Eliminar(int id)
        {
            Entity entidad = Get(id);

            if (entidad == null)
            {
                string error = string.Format("No existe el registro con el identificador {0}", id);
                throw new GenericException(error, error);
            }

            _repository.Delete(entidad);
        }


        public void Eliminar(Entity entity)
        {
            _repository.Delete(entity);
        }


        public virtual Entity Get(int id)
        {
            return _repository.Get(id);
        }

        public virtual IEnumerable<Entity> GetList()
        {
            return _repository.GetAll().ToList();
        }

        public virtual IEnumerable<Entity> GetList(Expression<Func<Entity, bool>> criteria)
        {
            return _repository.GetAll().Where(criteria).ToList();
        }

        public virtual IPagedListMetaData<Entity> GetList(Expression<Func<Entity, bool>> criteria, int skip, int take)
        {
            return _repository.GetList(criteria, skip, take);
        }

        public virtual IEnumerable<Entity> GetList(IList<FilterEntity> filters)
        {
            //TODO: Validar si filters es valido, y no es nulo
            return _repository.GetList(filters);
        }

        public virtual IPagedListMetaData<Entity> GetList(int Skip, int Take)
        {
            return _repository.GetList(Skip, Take);
        }

        public virtual IPagedListMetaData<Entity> GetList(IList<FilterEntity> filters, int Skip, int Take)
        {
            //TODO: Validar si filters es valido, y no es nulo
            return _repository.GetList(filters, Skip, Take);
        }


        public virtual Entity SaveOrUpdate(Entity entity)
        {
            return _repository.InsertOrUpdate(entity);
        }

        public virtual IList<Entity> SaveOrUpdate(IList<Entity> listEntity)
        {
            return _repository.SaveOrUpdate(listEntity);
        }

        public virtual long LongCount()
        {
            return _repository.LongCount();
        }

        public virtual long LongCount(Expression<Func<Entity, bool>> predicate)
        {
            return _repository.LongCount(predicate);
        }

        public IEnumerable<Entity> GetList(IList<FilterEntity> filters, string orderBy)
        {
            return _repository.GetList(filters, orderBy);

        }

        public IPagedListMetaData<Entity> GetList(IList<FilterEntity> filters, int Skip, int Take, string orderBy)
        {
            return _repository.GetList(filters, Skip, Take, orderBy);
        }
    }
}
