using System;
using System.IO;
 
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace com.cpp.calypso.framework.test
{
    [TestClass]
    public class FileRepositoryTemplatesTest
    {
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Si_Archivo_Plantilla_No_Encuentra_Lanzar_Excepcion()
        {

            var fileRepositoryTemplates = new FileRepositoryTemplates("foo");
            var result = fileRepositoryTemplates.GetContentTemplate("bar");

        }

        [TestMethod]
        public void Si_Archivo_Plantilla_Existe_Recuperar_Contenido()
        {
            var pathTemplate = "Templates";
            var template = "FooTemplate";

            var fileRepositoryTemplates = new FileRepositoryTemplates(pathTemplate);
            var result = fileRepositoryTemplates.GetContentTemplate(template);

            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void Si_Archivo_Plantilla_Diferente_Extesion_Existe_Recuperar_Contenido()
        {
            var extesion = "cshtml";
            var pathTemplate = "Templates";
            var template = "BarTemplate";

            var fileRepositoryTemplates = new FileRepositoryTemplates(pathTemplate, extesion);

            var result = fileRepositoryTemplates.GetContentTemplate(template);

            Assert.IsNotNull(result);
        }
    }
}
