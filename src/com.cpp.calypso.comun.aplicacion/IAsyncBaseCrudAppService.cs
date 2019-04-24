using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.cpp.calypso.comun.aplicacion
{



    public interface IAsyncBaseCrudAppService<TEntity, TEntityDto,TGetAllInput> :
    IAsyncBaseCrudAppService<TEntity, TEntityDto, TGetAllInput, TEntityDto>
    where TEntity : IEntity<int>
    where TEntityDto : IEntityDto<int>
    where TGetAllInput : IPagedAndSortedResultRequest
    {
  
        
    }

    public interface IAsyncBaseCrudAppService<TEntity, TEntityDto, TGetAllInput, in TCreateInput> :
        IAsyncCrudAppService<TEntityDto, int, TGetAllInput, TCreateInput>
       where TEntity : IEntity<int>
       where TEntityDto : IEntityDto<int>
       where TCreateInput : IEntityDto<int>
       where TGetAllInput : IPagedAndSortedResultRequest
    {

        /// <summary>
        /// Obtener todos los elementos
        /// </summary>
        /// <returns></returns>
        Task<IList<TEntityDto>> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        Task<TEntityDto> InsertOrUpdateAsync(TCreateInput entityDto);


        DateTime GetDateFromString(string date);

        string GetStringFromDate(DateTime? oDate);

        DateTime GetDateTimeFromString(string date);

        string GetStringFromDateTime(DateTime? oDate);

        string GetStringFromBool(bool b);
    }
}
