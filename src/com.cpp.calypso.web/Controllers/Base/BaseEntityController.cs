using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Mvc;
using Abp.Domain.Entities;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using CommonServiceLocator;
using FluentValidation;
using FluentValidation.Results;

namespace com.cpp.calypso.web
{
    /// <summary>
    /// Controller for entity
    /// </summary>
    public abstract class BaseEntityController<Entity> : BaseController where
        Entity : IEntity
    {
        private static readonly ILogger log =
       ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(BaseEntityController<>));

        /// <summary>
        /// Servicio de la entidad
        /// </summary>
        protected IEntityService<Entity> EntityService;

        protected IViewService ViewService;

        protected ICreateObject CreateObject;

        protected IApplication Application;

        protected IParametroService ParametroService;


        private bool _ApplySearch = false;
        /// <summary>
        /// If display panel search
        /// </summary>
        public bool ApplySearch
        {
            get { return _ApplySearch; }
            set { _ApplySearch = value; }
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
        /// TODO: TEMPORAL VER LA FORMA DE UTILIZAR ACTION/MENU. ODOO
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
        /// TODO: TEMPORAL VER LA FORMA DE UTILIZAR ACTION/MENU. ODOO
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
        /// Validador, para aplicar al momento de crear, editar las entidades
        /// </summary>
        protected AbstractValidator<Entity> Validator = null;


        protected BaseEntityController(IHandlerExcepciones manejadorExcepciones,
            IApplication application,
            ICreateObject createObject,
            IParametroService parametroService,
            IViewService viewService,
            IEntityService<Entity> entityService) :
            base(manejadorExcepciones)
        {
            Application = application;

            CreateObject = createObject;

            ParametroService = parametroService;

            ViewService = viewService;

            EntityService = entityService;
        }

        /// <summary>
        /// Obtener la vista tree
        /// </summary>
        /// <returns></returns>
        protected View GetViewTree()
        {

            if (string.IsNullOrEmpty(NameViewTree))
                return ViewService.Get(typeof(Entity), typeof(Tree));

            return ViewService.Get(NameViewTree);
        }

        /// <summary>
        /// Obtener la vista Search
        /// </summary>
        /// <returns></returns>
        protected View GetViewSearch()
        {

            if (string.IsNullOrEmpty(NameViewSearch))
                return ViewService.Get(typeof(Entity), typeof(Search));

            return ViewService.Get(NameViewSearch);
        }

        /// <summary>
        /// TODO: TEMPORAL VER LA FORMA DE UTILIZAR ACTION/MENU. ODOO
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
        /// Inicializar el objeto de la entidad, en la accion de crear.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public virtual Entity Initialize(Entity entity)
        {
            return entity;
        }


        /// <summary>
        /// Generic Index
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="TipoMensaje"></param>
        /// <returns></returns>
        public virtual ActionResult Index(string msg, TipoMensaje? TipoMensaje)
        {
            //1. Model
            var model = new TreeModelView();

            if (!string.IsNullOrEmpty(Title))
            {
                model.Title = Title;
            }

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

                IPagedListMetaData<Entity> result;
                if (string.IsNullOrWhiteSpace(treeView.DefaultOrder))
                {
                    result = EntityService.GetList(filters, (pageNumber - 1) * pageSize, pageSize);
                }
                else
                {
                    result = EntityService.GetList(filters, (pageNumber - 1) * pageSize, pageSize, treeView.DefaultOrder);
                }

                model.Model = result.Subset as IEnumerable<IEntity>;
                model.PagedListMetaData = new PagedListMetaDataModel(pageNumber, pageSize, result.TotalResultSetCount);

            }
            else
            {

                if (string.IsNullOrWhiteSpace(treeView.DefaultOrder))
                {
                    model.Model = EntityService.GetList(filters) as IEnumerable<IEntity>;
                }
                else
                {
                    model.Model = EntityService.GetList(filters, treeView.DefaultOrder) as IEnumerable<IEntity>;
                }

            }


            //4. View Search
            if (ApplySearch)
            {
                var searchView = GetViewSearch();

                //TODO: de la misma forma que formview, se puede generar metadatos..
                //Ejemplo si el filtro es un entidad.. (Visualizar el listado de esa entidad)
                //Si el campo... enum. (Crear un combo con las opciones posibles)
                //
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

            ////5. Pagination
            //if (ApplyPagination)
            //{
            //    model.PagedListMetaData = new PagedListMetaDataModel();
            //}

            return View(model);
        }


        [HttpPost]
        public virtual ActionResult Search(List<FilterEntity> filters, int? page) //, string orderBy)
        {
            var model = new TreeModelView();

            //Add Filtros personalizados
            filters = AddFilter(filters);



            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)

            var pageSize = ParametroService.GetValor<int>(CodigosParametros.PARAMETRO_TAMAÑO_PAGINA_GRILLAS);

            //1. Get View 
            var view = GetViewTree();
            var treeView = view.Layout as Tree;
            model.View = treeView;

            //2. Get Data
            IPagedListMetaData<Entity> result;
            if (string.IsNullOrWhiteSpace(treeView.DefaultOrder)) // && string.IsNullOrEmpty(orderBy))
            {
                result = EntityService.GetList(filters, (pageNumber - 1) * pageSize, pageSize);
            }
            else
            {
                //if (!string.IsNullOrEmpty(orderBy))
                //{
                //    result = EntityService.GetList(filters, (pageNumber - 1) * pageSize, pageSize, orderBy);
                //}
                //else {
                result = EntityService.GetList(filters, (pageNumber - 1) * pageSize, pageSize, treeView.DefaultOrder);
                //}
            }



            var list = result.Subset as IEnumerable<IEntity>;
            model.Model = list;
            model.PagedListMetaData = new PagedListMetaDataModel(pageNumber, pageSize, result.TotalResultSetCount);



            //3. Message
            model.Mensaje = new MensajeHelper();

            //4. Paged
            ////View Search
            //view = _viewService.Get(typeof(Entity), typeof(Search));
            ////var searchView = view.Layout as Search;
            ////view.Name
            //model.SearchView = view;

            // Order
            //model.OrderBy = !string.IsNullOrEmpty(orderBy) ? orderBy  : treeView.DefaultOrder;

            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("FormDynamic/TreeView", model)
                : View("Index", model);
        }



        /// <summary>
        /// Generic Create
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Create()
        {

            var modelView = new FormModelView();
            //1. View
            var view = ViewService.Get(typeof(Entity), typeof(Form));
            var formView = view.Layout as Form;
            modelView.View = formView;

            //2. Model
            modelView.Model = Initialize(CreateObject.CreateInstance<Entity>());

            //3. Metadata
            modelView.Metadata = GenerateMetadataView.Generate(formView, view.ModelType);



            return View(modelView);
        }

        /// <summary>
        /// Ejemplo para sobrecargar metodos de create
        /// 
        /// public override ActionResult Create() {
        ///     var result = base.Create() as ViewResult;
        ///     var model = result.ViewData.Model as FormModelView;
        ///     model.Metadata.Add("foo", "bar");
        ///     return result;
        /// }
        /// 
        /// </summary>
        /// <returns></returns>
        private ActionResult CreateOverride()
        {
            var result = Create() as ViewResult;
            var modelOriginal = result.ViewData.Model as FormModelView;

            modelOriginal.Metadata.Add("foo", "bar");
            return result;
        }

        // POST: 
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(Entity entity, FormCollection formCollection)
        {
            try
            {

                if (ModelState.IsValid)
                {


                    if (Validator != null)
                    {
                        ValidationResult result = Validator.Validate(entity);

                        if (result.IsValid)
                        {
                            EntityService.SaveOrUpdate(entity);

                            return RedirectToAction("Index", new { msg = "Proceso guardado exitosamente", TipoMensaje = TipoMensaje.Correcto });

                        }
                        else
                        {
                            foreach (ValidationFailure failer in result.Errors)
                            {
                                ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                            }
                        }
                    }
                    else
                    {

                        EntityService.SaveOrUpdate(entity);

                        return RedirectToAction("Index", new { msg = "Proceso guardado exitosamente", TipoMensaje = TipoMensaje.Correcto });
                    }

                }
            }
            catch (Exception ex)
            {
                var result = ManejadorExcepciones.HandleException(ex);
                ModelState.AddModelError("", result.Message);
            }

            var modelView = new FormModelView();
            modelView.Model = entity;

            var view = ViewService.Get(typeof(Entity), typeof(Form));
            var formView = view.Layout as Form;

            modelView.View = formView;

            //3. Metadata
            modelView.Metadata = GenerateMetadataView.Generate(formView, view.ModelType);

            return View(modelView);
        }


        /// <summary>
        /// Generic Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual ActionResult Edit(int? id)
        {

            //TODO: Si la vista (tree) no tiene la opcion edit, permitir editar??

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var entity = EntityService.Get(id.Value);
            if (entity == null)
            {
                var msg = string.Format("El Registro de {0} con identificacion {1} no existe, o sus datos asociados no existen",
                    typeof(Entity).GetDescription(), id.Value);

                return HttpNotFound(msg);
            }


            //Verificar si se puede eliminar. 
            var check = CanEdited(entity);
            if (!check.Item1)
            {
                return RedirectToAction("Index", new { msg = string.Format("El registro no se puede editar : {0}", check.Item2), TipoMensaje = TipoMensaje.Alerta });
            }

            var modelView = new FormModelView();
            modelView.Model = entity;

            var view = ViewService.Get(typeof(Entity), typeof(Form));
            var formView = view.Layout as Form;

            modelView.View = formView;

            //3. Metadata
            modelView.Metadata = GenerateMetadataView.Generate(formView, view.ModelType);

            return View(modelView);
        }


        // POST: Catalogo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(Entity entity, FormCollection formCollection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Validator != null)
                    {
                        ValidationResult result = Validator.Validate(entity);

                        if (result.IsValid)
                        {
                            EntityService.SaveOrUpdate(entity);

                            return RedirectToAction("Index", new { msg = "Proceso guardado exitosamente", TipoMensaje = TipoMensaje.Correcto });

                        }
                        else
                        {
                            foreach (ValidationFailure failer in result.Errors)
                            {
                                ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                            }
                        }
                    }
                    else
                    {
                        EntityService.SaveOrUpdate(entity);

                        return RedirectToAction("Index", new { msg = "Proceso guardado exitosamente", TipoMensaje = TipoMensaje.Correcto });
                    }
                }
            }
            catch (Exception ex)
            {
                var result = ManejadorExcepciones.HandleException(ex);
                ModelState.AddModelError("", result.Message);
            }


            var modelView = new FormModelView();
            modelView.Model = entity;

            var view = ViewService.Get(typeof(Entity), typeof(Form));
            var formView = view.Layout as Form;

            modelView.View = formView;

            //3. Metadata
            modelView.Metadata = GenerateMetadataView.Generate(formView, view.ModelType);

            return View(modelView);
        }


        // GET: 
        public virtual ActionResult Details(int? id)
        {
            log.DebugFormat("Details({0})", id.Value);

            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var entity = EntityService.Get(id.Value);
            if (entity == null)
            {
                var msg = string.Format("El Registro de {0} con identificacion {1} no existe, o sus datos asociados no existen",
                     typeof(Entity).GetDescription(), id.Value);

                return HttpNotFound(msg);
            }

            var modelView = new FormModelView();
            modelView.Model = entity;

            var view = ViewService.Get(typeof(Entity), typeof(Form));
            var formView = view.Layout as Form;

            modelView.View = formView;

            //3. Metadata
            //modelView.Metadata = GenerateMetadataView.Generate(formView, view.ModelType);

            return View(modelView);
        }


        // GET:
        public virtual ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = EntityService.Get(id.Value);
            if (entity == null)
            {
                var msg = string.Format("El Registro de {0} con identificacion {1} no existe, o sus datos asociados no existen",
                    typeof(Entity).GetDescription(), id.Value);

                return HttpNotFound(msg);
            }


            var modelView = new FormModelView();
            modelView.Model = entity;

            var view = ViewService.Get(typeof(Entity), typeof(Form));
            var formView = view.Layout as Form;

            modelView.View = formView;


            return View(modelView);
        }

        // POST: 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var entity = EntityService.Get(id);
                if (entity == null)
                {
                    var msg = string.Format("El Registro de {0} con identificacion {1} no existe, o sus datos asociados no existen",
                        typeof(Entity).GetDescription(), id);

                    return HttpNotFound(msg);
                }

                //Verificar si se puede eliminar. 
                var check = CanRemoved(entity);
                if (check.Item1)
                {

                    EntityService.Eliminar(id);

                    return RedirectToAction("Index", new { msg = "Proceso guardado exitosamente", TipoMensaje = TipoMensaje.Correcto });
                }

                return RedirectToAction("Index", new { msg = string.Format("El registro no se puede eliminar : {0}", check.Item2), TipoMensaje = TipoMensaje.Error });

            }
            catch (Exception ex)
            {
                var result = ManejadorExcepciones.HandleException(ex);

                return RedirectToAction("Index", new { msg = result.Message, TipoMensaje = TipoMensaje.Error });

            }

        }

        /// <summary>
        /// Generic Export
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public virtual ActionResult Export(List<FilterEntity> filters, string format)
        {

            //1. Data
            //var list= GetList();


            //2. Get View 
            var view = ViewService.Get(typeof(Entity), typeof(Tree));
            var treeView = view.Layout as Tree;


            string[] propiedades = (from m in treeView.Fields
                                    select m.Name).ToArray(); ;

            MemberInfo[] Members = null;

            BindingFlags memberFlags = BindingFlags.Public | BindingFlags.Instance;

            var type = typeof(Entity);
            Members = type.GetProperties(memberFlags);

            MemberInfo[] membersFilter = (from m in Members
                                          where propiedades.Any(val => m.Name.ToUpper() == val.ToUpper())
                                          select m).ToArray();


            var data = EntityService.GetList(filters).ToList();

            //TODO: ExportUtility.ExportExcel. Internamente se usa EPPlus el cual utiliza DescriptionAttribute de las propiedades 
            //pasadas "membersFilter" para establecer los nombres de las columnas
            //se deberia adaptar para soportar el valor de "String" del campo en las vistas 

            var file = ExportUtility.ExportExcel<Entity>(data, membersFilter);

            if (file != null)
            {
                //TODO: Nombre mas fecha ??
                var nameFile = string.Format("{0}.xlsx", typeof(Entity).GetDescription());

                return new FileContentResult(file,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                { FileDownloadName = nameFile };
            }

            return View("Index");
        }


        /// <summary>
        /// Verificar si la entidad puede crearse
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Tuple<bool, string> CanCreated(Entity entity)
        {

            return new Tuple<bool, string>(true, "ok");
        }

        /// <summary>
        /// Verificar si la entidad puede editarse
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Tuple<bool, string> CanEdited(Entity entity)
        {

            return new Tuple<bool, string>(true, "ok");
        }


        /// <summary>
        ///  Verificar si la entidad puede eliminarse
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract Tuple<bool, string> CanRemoved(Entity entity);


    }
}
