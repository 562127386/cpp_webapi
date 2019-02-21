using System;
using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.cpp.calypso.framework.test
{
    [TestClass]
    public class RazorTemplateEngineTest
    {
        [TestMethod]
        public async Task Si_Plantilla_No_Tiene_Refencias_Modelo_Procesar()
        {
            Random key = new Random();

            var templateEngine = new RazorTemplateEngine();
            var model = new MockModelTemplate();
            model.Nombre = "foo";
            model.Titulo = "Mock Template Engine";
   
            string template = "Content Template Foo";

            string result = await templateEngine.Process(key.Next().ToString(), template, model);

            Assert.AreEqual(result, "Content Template Foo");
        }


        [TestMethod]
        public async Task Si_Plantilla_Tiene_Refencias_Modelo_Procesar()
        {
            Random key = new Random();

            var templateEngine = new RazorTemplateEngine();
            var model = new MockModelTemplate();
            model.Nombre = "foo";
            model.Titulo = "Mock Template Engine";

            string template = "Hola @Model.Nombre, cual es tu @Model.Titulo";

            string result = await templateEngine.Process(key.Next().ToString(), template, model);

            Assert.AreEqual(result, "Hola foo, cual es tu Mock Template Engine");
        }


        [TestMethod]
        [ExpectedException(typeof(IncorrectTemplateException))] 
        public async Task Si_Plantilla_Tiene_Modelo_Propiedades_No_Existen_Lanzar_Error()
        {
            Random key = new Random();

            var templateEngine = new RazorTemplateEngine();
            var model = new MockModelTemplate();
            model.Nombre = "foo";
            model.Titulo = "Mock Template Engine";

            string template = "Hola @Model.Nombre, cual es tu @Model.Titulo. Tu ciudad es @Model.Ciudad";

            string result = await templateEngine.Process(key.Next().ToString(), template, model);

            Assert.AreEqual(result, "Hola foo, cual es tu Mock Template Engine");
        }

    }


    public class MockModelTemplate : DynamicObject
    {
        public MockModelTemplate()
        {
            
        }

        public string Titulo { get; set; }
        public string Nombre { get; set; } 
    }
}
