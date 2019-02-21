using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using CommonServiceLocator;
using FluentValidation;
using FluentValidation.Results;
 

namespace com.cpp.calypso.web
{
    public abstract class BaseCrudDtoController<TEntity, TEntityDto, TGetAllInput>
       : BaseCrudDtoController<TEntity, TEntityDto, TGetAllInput, TEntityDto>
        where TEntity : class, IEntity<int>
        where TEntityDto : class, IEntityDto<int>
        where TGetAllInput : PagedAndFilteredResultRequestDto

    {
        protected BaseCrudDtoController(IHandlerExcepciones manejadorExcepciones,
            IApplication application, ICreateObject createObject,
            IParametroService parametroService, 
            TGetAllInput getAllInput, 
            IViewService viewService,
            IAsyncBaseCrudAppService<TEntity, TEntityDto, TGetAllInput> entityService) : 
            base(manejadorExcepciones, application, createObject, parametroService, 
                getAllInput, viewService, entityService)
        {
            
        }
    }


    /// <summary>
    /// Controlador base de Crud (Visualizar, crear, editar, eliminar)
    /// </summary>
    public abstract class BaseCrudDtoController<TEntity, TEntityDto, TGetAllInput, TCreateInput>
     : BaseExportAndSearchDtoConttroller<TEntity, TEntityDto, TGetAllInput, TCreateInput>
        where TEntity : class, IEntity<int>
        where TEntityDto : class, IEntityDto<int>
        where TCreateInput : class, IEntityDto<int>
        where TGetAllInput : PagedAndFilteredResultRequestDto
    {
        private static readonly ILogger log =
       ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(BaseCrudDtoController<,,,>));


        #region Propiedades

        protected ICreateObject CreateObject;

        protected IApplication Application;

        #endregion


        public IPagedListMetaData<TEntityDto> result { get; private set; }

  
        /// <summary>
        /// Validador, para aplicar al momento de crear, editar las entidades
        /// </summary>
        protected AbstractValidator<TCreateInput> Validator = null;

        
        protected BaseCrudDtoController(IHandlerExcepciones manejadorExcepciones,
           IApplication application,
           ICreateObject createObject,
           IParametroService parametroService,
           TGetAllInput getAllInput,
           IViewService viewService,
           IAsyncBaseCrudAppService<TEntity, TEntityDto, TGetAllInput, TCreateInput> entityService) :
           base(manejadorExcepciones, parametroService, getAllInput, viewService, entityService)
        {
            Application = application;

            CreateObject = createObject;
        }



        /// <summary>
        /// Inicializar el objeto de la entidad, en la accion de crear.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public virtual TCreateInput Initialize(TCreateInput entity)
        {
            entity.Id = 0;
            return entity;
        }

        /// <summary>
        /// Generar metadatos. 
        /// Ejemplos:
        /// 1. Un listado de items, que se permita seleccionar para establecer el valor de un campo
        /// 2. Un listado de Enum
        /// 3. Un entidad de negocio, para asociarla a un campo
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, object> GenerateMetada(Form formView, Type modelType) {

            //Generar automaticamente los metadatos, segun el tipo de los campos
            return GenerateMetadataView.Generate(formView, modelType);
        }

        /// <summary>
        /// Generic Create
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ActionResult> Create()
        {
            return await Task.Run(() =>
            {
                var modelView = new FormModelView();

                //1. View
                var view = ViewService.Get(typeof(TCreateInput), typeof(Form));
                var formView = view.Layout as Form;
                modelView.View = formView;

                //2. Model
                var obj = Initialize(CreateObject.CreateInstance<TCreateInput>());
                modelView.ModelDto = obj as IEntityDto;

                //3. Metadata
                modelView.Metadata = GenerateMetada(formView, view.ModelType);

                return View(modelView);
            });        
        }

     

        // POST: 
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(TCreateInput entity, FormCollection formCollection) 
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
                            await Service.InsertOrUpdateAsync(entity);

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
                    else {

                        await Service.InsertOrUpdateAsync(entity);

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
            modelView.ModelDto = entity as IEntityDto;

            var view = ViewService.Get(typeof(TCreateInput), typeof(Form));
            var formView = view.Layout as Form;

            modelView.View = formView;

            //3. Metadata
            modelView.Metadata = GenerateMetadataView.Generate(formView, view.ModelType);

            return View(modelView);
        }


        /// <summary>
        /// Editar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual  async Task<ActionResult> Edit(int? id)
        {

            //TODO: Si la vista (tree) no tiene la opcion edit, permitir editar??

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var entity = await Service.Get(new EntityDto<int>(id.Value));
            if (entity == null)
            {
                var msg = string.Format("El Registro de {0} con identificacion {1} no existe, o sus datos asociados no existen",
                    typeof(TEntityDto).GetDescription(), id.Value);

                return HttpNotFound(msg);
            }


            //Verificar si se puede eliminar. 
            var check = CanEdited(entity);
            if (!check.Item1)
            {
                return RedirectToAction("Index", new { msg = string.Format("El registro no se puede editar : {0}", check.Item2), TipoMensaje = TipoMensaje.Alerta });
            }

            var modelView = new FormModelView();
            modelView.ModelDto = entity as IEntityDto;

            var view = ViewService.Get(typeof(TEntityDto), typeof(Form));
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
        public virtual async Task<ActionResult> Edit(TCreateInput entity, FormCollection formCollection)
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
                            await Service.InsertOrUpdateAsync(entity);

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
                    else {
                        await Service.InsertOrUpdateAsync(entity);

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
            modelView.ModelDto = entity as  IEntityDto;

            var view = ViewService.Get(typeof(TCreateInput), typeof(Form));
            var formView = view.Layout as Form;

            modelView.View = formView;

            //3. Metadata
            modelView.Metadata = GenerateMetadataView.Generate(formView, view.ModelType);

            return View(modelView);
        }

 
        // GET: 
        public virtual async Task<ActionResult> Details(int? id)
        {
            log.DebugFormat("Details({0})", id.Value);

            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var entity = await Service.Get(new EntityDto<int>(id.Value));
            if (entity == null)
            {
                var msg = string.Format("El Registro de {0} con identificacion {1} no existe, o sus datos asociados no existen",
                     typeof(TEntityDto).GetDescription(), id.Value);

                return HttpNotFound(msg);
            }

            var modelView = new FormModelView();
            modelView.ModelDto = entity as IEntityDto;

            var view = ViewService.Get(typeof(TCreateInput), typeof(Form));
            var formView = view.Layout as Form;

            modelView.View = formView;

            //3. Metadata
            //modelView.Metadata = GenerateMetadataView.Generate(formView, view.ModelType);
 
            return View(modelView);
        }


        // GET:
        public virtual async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var entity = await Service.Get(new EntityDto<int>(id.Value));
            if (entity == null)
            {
                var msg = string.Format("El Registro de {0} con identificacion {1} no existe, o sus datos asociados no existen",
                    typeof(TEntityDto).GetDescription(), id.Value);

                return HttpNotFound(msg);
            }


            var modelView = new FormModelView();
            modelView.ModelDto = entity as IEntityDto;

            var view = ViewService.Get(typeof(TCreateInput), typeof(Form));
            var formView = view.Layout as Form;

            modelView.View = formView;


            return View(modelView);
        }

        // POST: 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
               
                var entity = await Service.Get(new EntityDto<int>(id));
                if (entity == null)
                {
                    var msg = string.Format("El Registro de {0} con identificacion {1} no existe, o sus datos asociados no existen",
                        typeof(TEntityDto).GetDescription(), id);

                    return HttpNotFound(msg);
                }

                //Verificar si se puede eliminar. 
                var check = CanRemoved(entity);
                if (check.Item1) {

                    await Service.Delete(new EntityDto<int>(id));

                    return RedirectToAction("Index", new { msg = "Proceso guardado exitosamente", TipoMensaje = TipoMensaje.Correcto });
                }

                return RedirectToAction("Index", new { msg = string.Format("El registro no se puede eliminar : {0}", check.Item2), TipoMensaje = TipoMensaje.Error });

            }
            catch (Exception ex)
            {
                var result= ManejadorExcepciones.HandleException(ex);

                return RedirectToAction("Index", new { msg = result.Message, TipoMensaje = TipoMensaje.Error });

            }

        }

        

        /// <summary>
        /// Verificar si la entidad puede crearse
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Tuple<bool, string> CanCreated(TEntityDto entity)
        {
            //Aplicar reglas de Negocio
            return new Tuple<bool, string>(true, "ok");
        }

        /// <summary>
        /// Verificar si la entidad puede editarse
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Tuple<bool, string> CanEdited(TEntityDto entity)
        {
            //Aplicar reglas de negocio
            return new Tuple<bool, string>(true, "ok");
        }


        /// <summary>
        ///  Verificar si la entidad puede eliminarse
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract Tuple<bool, string> CanRemoved(TEntityDto entity);


    }


}
