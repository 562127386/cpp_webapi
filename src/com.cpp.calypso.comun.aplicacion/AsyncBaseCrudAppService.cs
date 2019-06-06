using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json.Linq;

namespace com.cpp.calypso.comun.aplicacion
{


    public class AsyncBaseCrudAppService<TEntity, TEntityDto, TGetAllInput> :
       AsyncBaseCrudAppService<TEntity, TEntityDto, TGetAllInput, TEntityDto>
       where TEntity : class, IEntity<int>, IEntity
       where TEntityDto : IEntityDto<int>
       where TGetAllInput : IPagedAndSortedResultRequest
    {
        public AsyncBaseCrudAppService(
            IBaseRepository<TEntity> repository) : base(repository)
        {
        }


    }


    public class AsyncBaseCrudAppService<TEntity, TEntityDto, TGetAllInput, TCreateInput> :
        AsyncCrudAppService<TEntity, TEntityDto, int, TGetAllInput, TCreateInput>,
        IAsyncBaseCrudAppService<TEntity, TEntityDto, TGetAllInput, TCreateInput>
       where TEntity : class, IEntity<int>, IEntity
       where TEntityDto : IEntityDto<int>
       where TCreateInput : IEntityDto<int>
       where TGetAllInput : IPagedAndSortedResultRequest
    {
        private IHandlerExcepciones manejadorExcepciones;


        public virtual IHandlerExcepciones ManejadorExcepciones
        {
            get => manejadorExcepciones; set => manejadorExcepciones = value;
        }

        public AsyncBaseCrudAppService(
            IBaseRepository<TEntity> repository) : base(repository)
        {
        }



        public override async Task<PagedResultDto<TEntityDto>> GetAll(TGetAllInput input)
        {
            CheckGetAllPermission();

            var query = CreateFilteredQuery(input);
            query = ApplyFilter(query, input);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);


            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            return new PagedResultDto<TEntityDto>(
                totalCount,
                entities.Select(MapToEntityDto).ToList()
            );
        }


        /// <summary>
        ///   apply filter if needed.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="input">The input.</param>
        protected virtual IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, TGetAllInput input)
        {
            //Try to sort query if available
            var filterInput = input as PagedAndFilteredResultRequestDto;
            if (filterInput != null && filterInput.Filter != null)
            {
                return query.Where(filterInput.Filter);
            }

            //No filter
            return query;
        }


        public virtual async Task<TEntityDto> InsertOrUpdateAsync(TCreateInput input)
        {
            var baseRepository = Repository as IBaseRepository<TEntity>;
            if (baseRepository == null)
            {
                throw new ArgumentException(string.Format("The repository should be type IBaseRepository<TEntity>. {0}", Repository.GetType()), "input");
            }

            var entity = MapToEntity(input);

            await baseRepository.InsertOrUpdateAsync(entity);

            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        public virtual async Task<IList<TEntityDto>> GetAll()
        {
            var items = await Repository.GetAllListAsync();
            return items.Select(MapToEntityDto).ToList();
        }


        public virtual DateTime GetDateFromString(string date)
        {
            DateTime oDate = DateTime.ParseExact(date, "yyyy-MM-dd", null);
            return oDate;
        }


        public virtual DateTime GetDateFromStringFormat(string date)
        {
            DateTime oDate = DateTime.ParseExact(date, "dd-MM-yyyy", null);
            var stringFormat = oDate.ToString("yyyy-MM-dd");
            var dateFormar = DateTime.ParseExact(stringFormat, "yyyy-MM-dd", null);
            return dateFormar;
        }

        public virtual string GetStringFromDate(DateTime? oDate)
        {
            if (oDate == null)
            {
                return "";
            }
            return String.Format("{0:yyyy-MM-dd}", oDate);
        }

        public virtual DateTime GetDateTimeFromString(string date)
        {
            DateTime oDate = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss", null);
            return oDate;
        }

        public virtual string GetStringFromTimeSpan(TimeSpan? oDate)
        {
            if (oDate == null)
            {
                return "";
            }
            var time = oDate.GetValueOrDefault().ToString(@"hh\:mm\:ss");
            return time;
            //return String.Format("{0:HH:mm:ss}", oDate);
        }

        public virtual TimeSpan GetTimeSpanFromString(string date)
        {
            var oDate = TimeSpan.ParseExact(date, "HH:mm:ss", null);
            return oDate;
        }

        public virtual DateTime GetDateTimeFromStringFormat(string date)
        {
            DateTime oDate = DateTime.ParseExact(date, "yyyy-MM-dd", null);
            return oDate;
        }

        public virtual string GetStringFromDateTime(DateTime? oDate)
        {
            if (oDate == null)
            {
                return "";
            }
            return String.Format("{0:yyyy-MM-dd HH:mm:ss}", oDate);
        }

        public virtual string GetStringFromBool(bool b)
        {
            return b ? "1" : "0";
        }
    }
}
