using Abp.Application.Services.Dto;
using com.cpp.calypso.comun.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.cpp.calypso.web
{
    
    public static class PagedResultDtoExtensions
    {
       
        //public static IPagedListMetaData<T> ToPagedListMetaData<T>(this PagedResultDto<T> input) where T : struct
        //{
        //    var newValue = new PagedListMetaData<T>();
        //    newValue.Subset = input.Items.ToList();
        //    newValue.TotalResultSetCount = input.TotalCount;
        //    return newValue;
        //}

        public static IPagedListMetaData<T> ToPagedListMetaData<T>(PagedResultDto<T> input) where T : struct
        {
            var newValue = new PagedListMetaData<T>();
            newValue.Subset = input.Items.ToList();
            newValue.TotalResultSetCount = input.TotalCount;
            return newValue;
        }
    }
}