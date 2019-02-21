//using com.cpp.calypso.comun.aplicacion;
//using com.cpp.calypso.comun.dominio;
//using com.cpp.calypso.framework;
//using com.cpp.calypso.proyecto.aplicacion;
//using System;
//using System.Web.Mvc;
//using System.Linq;

//namespace com.cpp.calypso.web.Areas.Proyecto
//{

//    public class FooController : BaseSearchDtoConttroller<proyecto.dominio.Foo, FooDto, ConsultaFooPagedAndFilteredResultRequestDto>
//    {
      
//        public FooController(IHandlerExcepciones manejadorExcepciones,
//            IParametroService parametroService, 
//            ConsultaFooPagedAndFilteredResultRequestDto getAllInput,
//            IViewService viewService, 
//            IAsyncBaseCrudAppService<proyecto.dominio.Foo, FooDto, ConsultaFooPagedAndFilteredResultRequestDto> entityService) : 
//            base(manejadorExcepciones, parametroService, getAllInput, viewService, entityService)
//        {


//            //CONFIGURACION
            
//            //1. Nombre de la Vista de Busqueda
//            NameViewSearch = "com.cpp.calypso.proyecto.View.Search.Foo";

//            //2. Nombre de la Vista de Proyecto
//            //NameViewTree = "View.Tres.Proyecto";
//        }

        

//        protected override View GetViewSearch()
//        {
//            //1. Opcion Crear una vista para busqueda..  Codigo.
//            //2. Opcion guardar el XML en la base de datos...

//            var view = ViewService.RegisterOrGet(NameViewSearch, (NameViewSearch) => {

//                var viewNew = ViewFactory.CreateViewSearch(NameViewSearch);
//                var layout = viewNew.Layout as Search;

//                var field = new FieldSearch();
//                field.Name = "Codigo";
//                //Defaul.. 
//                field.Operator = "==";
//                field.FieldType = typeof(string);
//                field.String = "Codigo";


//                layout.Fields.Add(field);

//                var fieldNombre = new FieldSearch();
//                fieldNombre.Name = "NOmbre";
//                //Defaul.. 
//                fieldNombre.Operator = "==";
//                fieldNombre.FieldType = typeof(string);
//                fieldNombre.String = "NOmbre ";


//                layout.Fields.Add(field);

//                return viewNew;
//            });

//            return view; 
//        }


//        protected override View GetViewTree()
//        {
//            //3. Opcion Autogenerada... 

//            var viewAutogenerada =  base.GetViewTree();

//            var layout = viewAutogenerada.Layout as Tree;

//            //TODO: Crear intefaz fluida, para recuperar los campos del Objeto.. 
//            //Con el fin de no quemar los nombres
//            //Ocultar campos
//            layout.Fields.Where(f => f.Name == "FechaFinal").SingleOrDefault().Invisible = true;

//            return viewAutogenerada;
//        }


   

//        // GET: Proyecto/Proyecto/Details/5
//        public ActionResult Details(int id)
//        {
//            var input = new ConsultaFooPagedAndFilteredResultRequestDto();
//            input.Codigo = "Demo";


//            var proyectoServicio = Service as FooAsyncBaseCrudAppService;
//            var modelView = proyectoServicio.Consultar(input);

//            return View(modelView);
//        }

//        // GET: Proyecto/Proyecto/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Proyecto/Proyecto/Create
//        [HttpPost]
//        public ActionResult Create(FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add insert logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: Proyecto/Proyecto/Edit/5
//        public ActionResult Edit(int id)
//        {
//            return View();
//        }

//        // POST: Proyecto/Proyecto/Edit/5
//        [HttpPost]
//        public ActionResult Edit(int id, FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add update logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: Proyecto/Proyecto/Delete/5
//        public ActionResult Delete(int id)
//        {
//            return View();
//        }

//        // POST: Proyecto/Proyecto/Delete/5
//        [HttpPost]
//        public ActionResult Delete(int id, FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add delete logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        public  Tuple<bool, string> CanRemoved(FooDto entity)
//        {
//            return new Tuple<bool, string>(true, "ok"); 
//        }
//    }
//}
