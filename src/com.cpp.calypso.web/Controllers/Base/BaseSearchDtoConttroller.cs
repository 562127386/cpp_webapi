using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using CommonServiceLocator;

namespace com.cpp.calypso.web
{

    /// <summary>
    ///  Controlador base de visualizacion y busqueda
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TGetAllInput"></typeparam>
    public abstract class BaseSearchDtoConttroller<TEntity, TEntityDto, TGetAllInput> :
        BaseSearchDtoConttroller<TEntity, TEntityDto, TGetAllInput, TEntityDto>
        where TEntity : class, IEntity<int>
        where TEntityDto : class, IEntityDto<int>
        where TGetAllInput : PagedAndFilteredResultRequestDto

    {
        protected BaseSearchDtoConttroller(IHandlerExcepciones manejadorExcepciones, 
            IParametroService parametroService,
            TGetAllInput getAllInput, 
            IViewService viewService,
            IAsyncBaseCrudAppService<TEntity, TEntityDto, TGetAllInput, TEntityDto> entityService) : 
            base(manejadorExcepciones, parametroService, getAllInput, viewService, entityService)
        {
        }
    }

    /// <summary>
    /// Controlador base de visualizacion y busqueda
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TGetAllInput"></typeparam>
    public abstract class BaseSearchDtoConttroller<TEntity, TEntityDto, TGetAllInput, TCreateInput> : BaseController
         where TEntity : class, IEntity<int>
         where TEntityDto : class, IEntityDto<int>
         where TCreateInput : IEntityDto<int>
         where TGetAllInput : PagedAndFilteredResultRequestDto
    {

        private static readonly ILogger log =
  ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(BaseSearchDtoConttroller<,,,>));


        protected IParametroService ParametroService;
        protected TGetAllInput GetAllInput;
        protected IViewService ViewService;
        protected IAsyncBaseCrudAppService<TEntity, TEntityDto, TGetAllInput, TCreateInput> Service;

        protected BaseSearchDtoConttroller(
            IHandlerExcepciones manejadorExcepciones,
            IParametroService parametroService,
            TGetAllInput getAllInput,
            IViewService viewService,
            IAsyncBaseCrudAppService<TEntity, TEntityDto, TGetAllInput, TCreateInput> entityService) : base(manejadorExcepciones)
        {
            this.ParametroService = parametroService;
            this.GetAllInput = getAllInput;
            this.ViewService = viewService;
            this.Service = entityService;

            //Default
            ApplySearch = true;
            ApplyPagination = true;
        }



        private bool _ApplyPagination = false;
        /// <summary>
        /// If pagination is displayed
        /// </summary>
        public bool ApplyPagination
        {
            get { return _ApplyPagination; }
            set { _ApplyPagination = value; }
        }

        private bool _ApplySearch = false;

        /// <summary>
        /// Si se visualiza el la vista de busqueda
        /// </summary>
        public bool ApplySearch
        {
            get { return _ApplySearch; }
            set { _ApplySearch = value; }
        }

        /// <summary>
        /// Configuraciones de Contexto, etc.
        /// </summary>
        private string _NameViewTree = string.Empty;
        /// <summary>
        ///  Nombre de vista tipo Tree (lista), que se utilizada para visualizar listado del modelo.
        ///  Si no esta definido, se buscara la vista por defecto 
        /// </summary>
        public string NameViewTree
        {
            get { return _NameViewTree; }
            set { _NameViewTree = value; }
        }

        /// <summary>
        /// Configuraciones de Contexto, etc.
        /// </summary>
        private string _NameViewSearch = string.Empty;
        /// <summary>
        ///  Nombre de vista tipo Search , que se utilizada para generar bloque de busqueda
        ///  Si no esta definido, se buscara la vista por defecto 
        /// </summary>
        public string NameViewSearch
        {
            get { return _NameViewSearch; }
            set { _NameViewSearch = value; }
        }

        /// <summary>
        /// Titulo del formulario de listado
        /// </summary>
        private string _Title = string.Empty;
        /// <summary>
        /// Titulo del Formulario de Listado (Tree), si no se especifica, se obtiene desde la descripcion del modelo
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }


        /// <summary>
        /// Configuraciones de Contexto, etc.
        /// 
        /// Agregar Filtros, por defecto, filtros de contexto. Aplicar filtros condicionados, para visualizar 
        /// ciertos elementos del modelo, por reglas de Negocio. Ejemplo Partner (clientes, proveedores, estudiantes, contactos,etc). 
        /// visualizar unicamente proveedores,
        /// 
        /// Estos filtros son adicionales a los que aplica el usuario en UI.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual List<FilterEntity> AddFilter(List<FilterEntity> filterList)
        {
            return filterList;
        }
 

        /// <summary>
        /// Generic Index
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="TipoMensaje"></param>
        /// <returns></returns>
        public async virtual Task<ActionResult> Index(string msg, TipoMensaje? TipoMensaje)
        {
            //1. Model
            var model = new TreeModelView();

            if (!string.IsNullOrEmpty(Title))
            {
                model.Title = Title;
            }

            //Agregar filtros por defecto
            var filters = AddFilter(new List<FilterEntity>());

            //2. Get View 
            var view = GetViewTree();

            var treeView = view.Layout as Tree;
            model.View = treeView;

            //3. Get Data
            if (ApplyPagination)
            {
                var pageNumber = 1;
                var pageSize = ParametroService.GetValor<int>(CodigosParametros.PARAMETRO_TAMAÑO_PAGINA_GRILLAS);


                GetAllInput.SkipCount = pageNumber - 1;
                GetAllInput.MaxResultCount = pageSize;
                GetAllInput.Filter = filters;


                if (!string.IsNullOrWhiteSpace(treeView.DefaultOrder))
                {
                    GetAllInput.Sorting = treeView.DefaultOrder;
                }

                var pagedResultDto = await Service.GetAll(GetAllInput);

                model.ModelDto = pagedResultDto.Items as IEnumerable<IEntityDto>;
                model.PagedListMetaData = new PagedListMetaDataModel(pageNumber, pageSize, pagedResultDto.TotalCount);

            }
            else
            {

                GetAllInput.Filter = filters;

                if (!string.IsNullOrWhiteSpace(treeView.DefaultOrder))
                {
                    GetAllInput.Sorting = treeView.DefaultOrder;
                }

                var pagedResultDto = await Service.GetAll(GetAllInput);
                model.ModelDto = pagedResultDto.Items as IEnumerable<IEntityDto>;
            }


            //4. View Search
            if (ApplySearch)
            {
                var searchView = GetViewSearch();

                //Generar metadatos..
                //Ejemplo si el filtro es un entidad.. (Visualizar el listado de esa entidad)
                //Si el campo... enum. (Crear un combo con las opciones posibles)
                //model.MetadataSearchView = GenerateMetadataView.Generate(searchView, view.ModelType);

                //var searchView = view.Layout as Search;
                //view.Name
                model.SearchView = searchView;
            }


            //5. Message
            model.Mensaje = new MensajeHelper();
            if (TipoMensaje.HasValue)
            {
                model.Mensaje.Texto = msg;
                model.Mensaje.Tipo = TipoMensaje.Value;
            }

            // Order
            model.OrderBy = treeView.DefaultOrder;

            return View(model);
        }


        /// <summary>
        /// Obtener la vista tree
        /// </summary>
        /// <returns></returns>
        protected virtual View GetViewTree()
        {

            if (string.IsNullOrEmpty(NameViewTree))
                return ViewService.Get(typeof(TEntityDto), typeof(Tree));

            return ViewService.Get(NameViewTree);
        }

        /// <summary>
        /// Obtener la vista Search
        /// </summary>
        /// <returns></returns>
        protected virtual View GetViewSearch()
        {

            if (string.IsNullOrEmpty(NameViewSearch))
                return ViewService.Get(typeof(TEntityDto), typeof(Search));

            return ViewService.Get(NameViewSearch);
        }


        [HttpPost]
        public virtual async Task<ActionResult> Search(List<FilterEntity> filters, int? page) //, string orderBy)
        {
            var model = new TreeModelView();

            //Agregar filtros por defecto
            filters = AddFilter(filters);

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)

            var pageSize = ParametroService.GetValor<int>(CodigosParametros.PARAMETRO_TAMAÑO_PAGINA_GRILLAS);

            //1. Get View 
            var view = GetViewTree();
            var treeView = view.Layout as Tree;
            model.View = treeView;

            //2. Get Data
            GetAllInput.SkipCount = pageNumber - 1;
            GetAllInput.MaxResultCount = pageSize;
            GetAllInput.Filter = filters;


            if (!string.IsNullOrWhiteSpace(treeView.DefaultOrder))
            {
                GetAllInput.Sorting = treeView.DefaultOrder;
            }

            var pagedResultDto = await Service.GetAll((TGetAllInput)GetAllInput);

            model.ModelDto = pagedResultDto.Items as IEnumerable<IEntityDto>;
            model.PagedListMetaData = new PagedListMetaDataModel(pageNumber, pageSize, pagedResultDto.TotalCount);


            //3. Message
            model.Mensaje = new MensajeHelper();

            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("FormDynamic/TreeView", model)
                : View("Index", model);
        }

    }


}
