using System.Collections.Generic;
using System.Web.Mvc;
using System;
using CommonServiceLocator;
using com.cpp.calypso.framework;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.comun.aplicacion;

namespace com.cpp.calypso.webapi
{
    /// <summary>
    /// Permite crear una lista de filtros "List<FilterEntity>()", a partir de los datos proporcionados en ValueProvider. (Form, QueryString, Session, Cookie, etc)
    /// 
    /// Para determinar que filtros se deben crear, se verificar si existe en los datos proporcionados en ValueProvider el nombre de la vista de busqueda.
    /// El nombre de la vista, es insertado al momento de procesar la plantilla de vistas de busquedas. 
    /// 
    /// 
    /// Register:
    /// ModelBinders.Binders.Add(typeof(List<FilterEntity>), new FilterEntityModelBinder(viewService));
    /// </summary>
    public class FilterEntityModelBinder : IModelBinder
    {
        static readonly ILogger log =
ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(FilterEntityModelBinder));


        protected IViewService _viewService;

        public FilterEntityModelBinder(
            IViewService viewService)
        {
            _viewService = viewService;
        }

        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext", "controllerContext is null.");
            if (bindingContext == null)
                throw new ArgumentNullException("bindingContext", "bindingContext is null.");


            //bindingContext.ModelType

            //nuc_view_search_name
            var valueProvider = bindingContext.ValueProvider;

            log.DebugFormat("Type valueProvider : {0}", valueProvider.GetType());

            //TODO: nuc_view_search_name, colocar en constante.....

            var value = valueProvider.GetValue("nuc_view_search_name");
            if (value == null)
                return null;
 
            var view_search_name = value.AttemptedValue;

            if (string.IsNullOrWhiteSpace(view_search_name))
                return null;

            //TODO: throw  not found view ???
            //Registrar excepction only
            var view = _viewService.Get(view_search_name);
            if (view == null)
                return null;
 
            var searchView = view.Layout as Search;
            if (searchView == null) {
                log.DebugFormat("View {0} not type search, type is  {1}",view.Name, view.Layout.GetType());
                return null;
            }

            var filters = new List<FilterEntity>();

            //TODO: Agregar los filtros que estan asociados a la view search... 

            //Create list FilterEntity
            foreach (var item in searchView.Fields)
            {
                //Recuperar un valor, si existe el nombre del campo de la vista "search" en el listado 
                //de valores enviados desde el cliente... (formularios, string_query, etc...)
                value = valueProvider.GetValue(item.Name);

                log.DebugFormat("ValueProvider.GetValue({0})", item.Name);

                if (value != null && !string.IsNullOrEmpty(value.AttemptedValue)) {

                    //log.DebugFormat("ValueProvider.AttemptedValue : {0} ", value.AttemptedValue);
                    //log.DebugFormat("ValueProvider.RawValue : {0} ", value.RawValue);

                    var filter = new FilterEntity();
                    filter.Field = item.Name;
                    filter.Operator = item.Operator;
                    
                    //TODO: Convertir Value a type Field ??
                    filter.Value = value.AttemptedValue;
                    filters.Add(filter);
                }
            }

            //TODO: Process Filter
            filters = filters.ProcessFilter(view.ModelType);
            
            return filters;
        }
    }
}