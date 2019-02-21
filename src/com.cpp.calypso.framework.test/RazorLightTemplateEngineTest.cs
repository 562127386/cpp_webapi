using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorLight;

namespace com.cpp.calypso.framework.test
{
    [TestClass]
    public class RazorLightTemplateEngineTest
    {
        private TestContext m_testContext;
        public TestContext TestContext
        {
            get { return m_testContext; }
            set { m_testContext = value; }
        }

        [TestMethod]
        public async Task UseRazorLightDirect() {

            //Test. Exist error for:
            //GetEntryAssembly returns null?
            var nulo = Assembly.GetEntryAssembly();


            var engine = new RazorLightEngineBuilder()
              //.UseMemoryCachingProvider()
              .Build();

            string template = "Hello, @Model.Name. Welcome to RazorLight repository";
            ViewModel model = new ViewModel() { Name = "John Doe" };

            string result = await engine.CompileRenderAsync("templateKey", template, model);

            TestContext.WriteLine(result);

          

        }

        [TestMethod]
        public async Task Si_Plantilla_No_Tiene_Refencias_Modelo_Procesar()
        {
            Random key = new Random();

            var templateEngine = new RazorLightTemplateEngine("Templates");
            var model = new MockModelTemplate();
            model.Nombre = "foo";
            model.Titulo = "Mock Template Engine";

            string template = "Content Template Foo";

            string result = await templateEngine.Process(key.Next().ToString(), template, model);

            Assert.AreEqual(result, "Content Template Foo");
        }
    }

    internal class ViewModel
    {
        public string Name { get; internal set; }
    }
}
