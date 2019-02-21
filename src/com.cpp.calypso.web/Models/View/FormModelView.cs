using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.web
{
    /// <summary>
    /// Model View
    /// </summary>
    public class FormModelView : IViewModel
    {
        /// <summary>
        /// Model
        /// </summary>
        public IEntity Model;

        /// <summary>
        /// View
        /// </summary>
        public Form View;

        /// <summary>
        /// Metadata view
        /// </summary>
        public Dictionary<string, object> Metadata;

        /// <summary>
        /// 
        /// </summary>
        public IEntityDto ModelDto { get; internal set; }
    }
}