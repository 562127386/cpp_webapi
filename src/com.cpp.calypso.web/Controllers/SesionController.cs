using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using com.cpp.calypso.seguridad.aplicacion;
using System;

namespace com.cpp.calypso.web
{
    public class SesionController : BaseEntityController<Sesion>
    {

        private ISesionService  _sesionService;
        
        

        public SesionController(
            IHandlerExcepciones manejadorExcepciones,
            ISesionService sesionService, 
            IParametroService parametroService,
             ICreateObject createObject,
             IViewService viewService,
            IApplication application)
            : base(manejadorExcepciones,application, createObject, parametroService, viewService, sesionService)
        {
            _sesionService = sesionService;

            //Configuration
            ApplySearch = true;
            ApplyPagination = true;
           
        }

        public override Tuple<bool, string> CanRemoved(Sesion entity)
        {
            return new Tuple<bool, string>(false, "Las sesiones no pueden ser eliminados");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //_repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
