using Abp.Application.Services.Dto;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.framework;
using com.cpp.calypso.seguridad.aplicacion;
using CommonServiceLocator;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace com.cpp.calypso.web.Areas.Seguridad
{
    //TODO:  [AllowAnonymous] temporal
    //[AllowAnonymous]
    public class AuditoriaController :
        BaseSearchDtoConttroller<AuditoriaEntidad, AuditoriaDto, PagedAndFilteredResultRequestDto>

    {

        private static readonly ILogger log =
    ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(AuditoriaController));


        public AuditoriaController(IHandlerExcepciones 
            manejadorExcepciones,
            IParametroService parametroService,
            PagedAndFilteredResultRequestDto getAllInput, 
            IViewService viewService,
            IAuditoriaService entityService) :
            base(manejadorExcepciones, parametroService, getAllInput, viewService, entityService)
        {
        }


     
        public virtual async Task<ActionResult> Details(int? id)
        {
            log.DebugFormat("Details({0})", id.Value);

            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           
            var entity = await ((IAuditoriaService)Service).Get(new EntityDto<int>(id.Value));
            if (entity == null)
            {
                var msg = string.Format("El Registro de {0} con identificacion {1} no existe, o sus datos asociados no existen",
                     typeof(AuditoriaEntidad).GetDescription(), id.Value);

                return HttpNotFound(msg);
            }

            return View(entity); 
        }
    }
}