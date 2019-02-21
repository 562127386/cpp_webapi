using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;

namespace com.cpp.calypso.web
{
    public class ParametroSistemaController : BaseEntityController<ParametroSistema>
    {
        private readonly IBaseRepository<ParametroSistema> repository;
        private readonly IBaseRepository<Usuario> repositoryUsuario;

        public ParametroSistemaController(IHandlerExcepciones manejadorExcepciones,
             ICreateObject createObject,
              IParametroService parametroService,
              IBaseRepository<ParametroSistema> repository,
              IBaseRepository<Usuario> repositoryUsuario,
            IApplication application,
            IViewService viewService)
            : base(manejadorExcepciones, application, createObject, parametroService, viewService, parametroService)
        {
            
           
            //Configuraciones
            Validator = new ParametroSistemaValidator();
            this.repository = repository;
            this.repositoryUsuario = repositoryUsuario;
        }


        public override Tuple<bool, string> CanRemoved(ParametroSistema entity)
        {
            return new Tuple<bool, string>(false, "Los parametros no pueden ser eliminados");
        }


   


        // GET: ParametroSistema/Details/5
        public override ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var paramQuery = repository.GetAll();
            var usuarioQuery = repositoryUsuario.GetAll();

            var model = (from param in paramQuery
                        join usuario in usuarioQuery
                        on param.CreatorUserId equals usuario.Id
                        join usuarioAct in usuarioQuery
                        on param.LastModifierUserId equals usuarioAct.Id into usuarioTemp
                         from usuarioActLeft in usuarioTemp.DefaultIfEmpty()
                        where param.Id == id.Value
                        select new ParametroDetalleViewModel
                        {
                            Id = param.Id,
                            Nombre = param.Nombre,
                            Tipo = param.Tipo,
                            Categoria = param.Categoria,
                            Codigo = param.Codigo,
                            Descripcion = param.Descripcion,
                            EsEditable = param.EsEditable,
                            FechaActualizacion = param.LastModificationTime,
                            FechaCreacion = param.CreationTime,
                            Valor = param.Valor,
                            UsuarioCreacion = usuario.Cuenta,
                            UsuarioActualizacion = (usuarioActLeft != null) ? usuarioActLeft.Cuenta : string.Empty
                        }).FirstOrDefault();
 
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

         


        //// POST: ParametroSistema/Edit/5
        //// Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        //// más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public override ActionResult Edit(ParametroSistema parametroSistema,FormCollection formCollection)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            //TODO: Generic Validator if exits for entity
        //            //Se puede establecer como propiedad... ...
        //            ParametroSistemaValidator validator = new ParametroSistemaValidator();
        //            ValidationResult result = validator.Validate(parametroSistema);

        //            if (result.IsValid)
        //            {
        //                _parametroService.SaveOrUpdate(parametroSistema);

        //                return RedirectToAction("Index");

        //            }
        //            else { 
        //                foreach (ValidationFailure failer in result.Errors)
        //                {
        //                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        var result = ManejadorExcepciones.HandleException(ex);
        //        ModelState.AddModelError("", result.Message);

        //    }

        //    var modelView = new FormModelView();
        //    modelView.Model = parametroSistema;

        //    var view = _viewService.Get(typeof(ParametroSistema), typeof(Form));
        //    var formView = view.Layout as Form;

        //    modelView.View = formView;

        //    return View(modelView); 
            
        //}

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

       

    }
}
