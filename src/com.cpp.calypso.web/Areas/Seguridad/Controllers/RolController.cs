
using Abp.Application.Services.Dto;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using com.cpp.calypso.seguridad.aplicacion;
using CommonServiceLocator;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace com.cpp.calypso.web.Areas.Seguridad
{

    public class RolController : 
        BaseCrudDtoController<Rol,RolDto, PagedAndFilteredResultRequestDto>
    {

        private static readonly ILogger log =
   ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(RolController));

        
        private IFuncionalidadService FuncionalidadService;

        public RolController(IHandlerExcepciones manejadorExcepciones, 
            IApplication application,
            ICreateObject createObject, 
            IParametroService parametroService, 
            PagedAndFilteredResultRequestDto getAllInput,
            IFuncionalidadService  funcionalidadService,
            IViewService viewService,
            IRolService entityService) :
            base(manejadorExcepciones, application, createObject,
                parametroService,
                getAllInput, viewService, entityService)
        {
            FuncionalidadService = funcionalidadService;

            ///Configuration
            ApplySearch = false;
            ApplyPagination = false;
        }

  

        // GET: Rol/Details/
        public override async Task<ActionResult> Details(int? id)
        {
             
            log.DebugFormat("Details({0})", id.Value);

            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var rolSerice = Service as IRolService;

            var entity = await rolSerice.GetDetalle(id.Value);
            if (entity == null)
            {
                var msg = string.Format("El Registro de {0} con identificacion {1} no existe, o sus datos asociados no existen",
                     "Rol", id.Value);

                return HttpNotFound(msg);
            }

            var modelView = new FormModelView();
            modelView.ModelDto = entity as IEntityDto;

            var view = ViewService.Get(typeof(RolDetalleDto), typeof(Form));
            var formView = view.Layout as Form;

            modelView.View = formView;

       
            return View(modelView);
        }



        public async Task<ActionResult> CreatePermiso(int id)
        {
            var rolService = Service as IRolService;
            var rol = await rolService.GetRolAndPermissions(id);
 
            var model = new PermisoViewModel();
            model.Rol = rol;
            model.Funcionalidades = FuncionalidadService.GetFuncionalidades();
            return View(model);
        }


        
        // POST: Permisos/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePermiso(int id, int[] selectedAccion)
        {
           
            try
            {
                if (ModelState.IsValid)
                {

                    var rolService = Service as IRolService;

                    rolService.UpdatePermissions(id,selectedAccion);
 
                    return RedirectToAction("Index", new { msg = "Proceso guardado exitosamente", TipoMensaje = TipoMensaje.Correcto });
                }
            }
            catch (Exception ex)
            {
                var result = ManejadorExcepciones.HandleException(ex);
                ModelState.AddModelError("", result.Message);
            }

            return RedirectToAction("Index", new { msg = ModelState.ToSerializedString(), TipoMensaje = TipoMensaje.Error });

        }


        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                //_repositoryRol.Dispose();
            }
            base.Dispose(disposing);
        }



        public override  Tuple<bool, string> CanRemoved(RolDto input)
        {
            var rolService = Service as IRolService;

            var task  = rolService.CanRemoved(input);
            task.Wait();
            return task.Result;
        }

          
    }
}
