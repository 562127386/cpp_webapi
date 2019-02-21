//using com.cpp.calypso.comun.dominio;
//using System;
//using System.IO;
//using System.Web;
//using System.Web.Mvc;

//namespace com.cpp.calypso.web
//{
//    /// <summary>
//    /// Binder para parametros
//    /// </summary>
//    public class ParametroSistemaModelBinder : DefaultModelBinder
//    {
//        /// <summary>
//        /// Service para la gestion de archivos
//        /// </summary>
//        private readonly IFileStorage _storage;

//        public ParametroSistemaModelBinder(IFileStorage storage) {
//            _storage = storage;
//        }
        

//        public override object BindModel(ControllerContext controllerContext,
//            ModelBindingContext bindingContext)
//        {
//            var model = base.BindModel(controllerContext, bindingContext);

//            var param = model as ParametroSistema;
//            if (param == null) {
//                throw new ArgumentNullException("ParametroSistemaModelBinder", "El modelo en el ParametroSistemaModelBinder, debe ser de tipo [ParametroSistema]");
//            }

//            //Archivos
//            if (param.Tipo == TipoParametro.Imagen) {

//                var files = controllerContext.HttpContext.Request.Files;
//                HttpPostedFileBase ImagenFile = null;

//                for (int i = 0; i < files.Count; i++)
//                {
//                    var name = files.AllKeys[i];
//                    if (name.ToUpper() == "VALOR") {
//                        ImagenFile = files[i];
//                        break;
//                    }
//                }

//                if (ImagenFile == null) {
//                    throw new ArgumentNullException("ParametroSistema.Valor", "El valor del campo [Valor], no es de tipo HttpPostedFileBase");
//                }
              

//                var nombreFile = string.Format("{0}{1}", Guid.NewGuid(), Path.GetExtension(ImagenFile.FileName));
//                var result =  _storage.SaveFileAsync(nombreFile, ImagenFile.InputStream);

//                if (!result.Result)
//                {
//                    var error = "Inconvenientes para guardar el archivo";
//                    throw new GenericException(error, error);
//                }

//                param.Valor = nombreFile;
//            }

//            return model;

//        }
       
//    }
//}
