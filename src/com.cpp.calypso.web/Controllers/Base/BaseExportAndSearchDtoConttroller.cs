using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;

namespace com.cpp.calypso.web
{
    /// <summary>
    /// Controlador base de visualizacion, busqueda, exportacion de datos
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TGetAllInput"></typeparam>
    public abstract class BaseExportAndSearchDtoConttroller<TEntity, TEntityDto, TGetAllInput> :
       BaseExportAndSearchDtoConttroller<TEntity, TEntityDto, TGetAllInput, TEntityDto>
       where TEntity : class, IEntity<int>
       where TEntityDto : class, IEntityDto<int>
       where TGetAllInput : PagedAndFilteredResultRequestDto
    {
        public BaseExportAndSearchDtoConttroller(IHandlerExcepciones manejadorExcepciones,
            IParametroService parametroService, TGetAllInput getAllInput,
            IViewService viewService, IAsyncBaseCrudAppService<TEntity, TEntityDto, TGetAllInput, TEntityDto>
            entityService) : base(manejadorExcepciones, parametroService, getAllInput, viewService, entityService)
        {
        }
    }

    /// <summary>
    /// Controlador base de visualizacion, busqueda, exportacion de datos
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TGetAllInput"></typeparam>
    public abstract class BaseExportAndSearchDtoConttroller<TEntity, TEntityDto, TGetAllInput, TCreateInput> :
        BaseSearchDtoConttroller<TEntity, TEntityDto, TGetAllInput, TCreateInput>
        where TEntity : class, IEntity<int>
        where TEntityDto : class, IEntityDto<int>
        where TCreateInput : IEntityDto<int>
        where TGetAllInput : PagedAndFilteredResultRequestDto
    {
        public BaseExportAndSearchDtoConttroller(IHandlerExcepciones manejadorExcepciones,
            IParametroService parametroService, TGetAllInput getAllInput,
            IViewService viewService, IAsyncBaseCrudAppService<TEntity, TEntityDto, TGetAllInput, TCreateInput> entityService) :
            base(manejadorExcepciones, parametroService, getAllInput, viewService, entityService)
        {

        }

 
        /// <summary>
        /// Generic Export
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public virtual async Task<ActionResult> Export(List<FilterEntity> filters, string format)
        {

            //1. Data
            //var list= GetList();


            //2. Get View 
            var view = GetViewTree();
            var treeView = view.Layout as Tree;


            string[] propiedades = (from m in treeView.Fields
                                    select m.Name).ToArray(); ;

            MemberInfo[] Members = null;

            BindingFlags memberFlags = BindingFlags.Public | BindingFlags.Instance;

            var type = typeof(TEntityDto);
            Members = type.GetProperties(memberFlags);

            MemberInfo[] membersFilter = (from m in Members
                                          where propiedades.Any(val => m.Name.ToUpper() == val.ToUpper())
                                          select m).ToArray();

            GetAllInput.Filter = filters;
            var data = await Service.GetAll((TGetAllInput)GetAllInput);

 
            //TODO: ExportUtility.ExportExcel. Internamente se usa EPPlus el cual utiliza DescriptionAttribute de las propiedades 
            //pasadas "membersFilter" para establecer los nombres de las columnas
            //se deberia adaptar para soportar el valor de "String" del campo en las vistas 
            var items = data.Items;
            var file = ExportUtility.ExportExcel<TEntityDto>(items.ToList(), membersFilter);

            if (file != null)
            {
                //TODO: Nombre mas fecha ??
                var nameFile = string.Format("{0}.xlsx", typeof(TEntityDto).GetDescription());

                return new FileContentResult(file,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                { FileDownloadName = nameFile };
            }

            return View("Index");
        }
    }


}
