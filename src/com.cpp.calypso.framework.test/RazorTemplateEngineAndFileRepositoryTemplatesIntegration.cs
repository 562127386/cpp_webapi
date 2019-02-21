using System;
using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.cpp.calypso.framework.test
{
    [TestClass]
    public class RazorTemplateEngineAndFileRepositoryTemplatesIntegration
    {
        [TestMethod]
        public async Task Recuperar_Archivo_Procesar_Plantilla()
        {
            var extesion = "cshtml";
            var pathTemplate = "Templates";
            var templateCode = "BarTemplate";

            var fileRepositoryTemplates = new FileRepositoryTemplates(pathTemplate, extesion);
            var template = fileRepositoryTemplates.GetContentTemplate(templateCode);

            var templateEngine = new RazorTemplateEngine();
            var model = new MockModelTemplate();
            model.Nombre = "foo";
            model.Titulo = "Mock Template Engine";
          
            string result = await templateEngine.Process("bar", template, model);

            string resultFinal = @"<html>  
<head> 
<title>Mock Template Engine</title>   
</head> 
<body>Nombre: foo </body>   
</html>";

            Assert.AreEqual(result, resultFinal);
        }

        [TestMethod]
        public async Task Generar_Codigo_Tracking_GoogleAnalytics()
        {
            Random key = new Random();

            var extesion = "cshtml";
            var pathTemplate = "Templates";
            var templateCode = "TrackingGoogleAnalytics";

            var fileRepositoryTemplates = new FileRepositoryTemplates(pathTemplate, extesion);
            var template = fileRepositoryTemplates.GetContentTemplate(templateCode);

            var templateEngine = new RazorTemplateEngine();
            var model = new ModelTracking();
            model.UserId = "foo";
            model.KeyApi = "12345678";

            string result = await templateEngine.Process(key.Next().ToString(), template, model);

            Console.WriteLine(result);
 
        }
 
  }

    public class ModelTracking : DynamicObject
    {
        public ModelTracking()
        {

        }

        public string UserId { get; set; }
        public string KeyApi { get; set; }
    }

    
}
