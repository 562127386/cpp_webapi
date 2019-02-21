using com.cpp.calypso.comun.dominio;
using System;
using System.IO;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Clase para manejo de repositorios de templates en archivos
    /// </summary>
    public class FileRepositoryTemplates : IRepositoryTemplates
    {
        /// <summary>
        /// Extension de los archivos de las plantillas
        /// </summary>
        private string _ExtensionFileTemplate = "cshtml";

        /// <summary>
        /// Path templates
        /// </summary>
        private string _TemplatePath;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="templatesPath"></param>
        public FileRepositoryTemplates(string templatesPath)
        {
            _TemplatePath = templatesPath; 
        }

        public FileRepositoryTemplates(string templatesPath,string extensionFile) {

            _TemplatePath = templatesPath;
            _ExtensionFileTemplate = extensionFile;
        }
 


        /// <summary>
        /// Obtener el contenido del template desde un archivo
        /// </summary>
        /// <returns></returns>
        public string GetContentTemplate(string codeTemplate)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string relativeSearchPath = AppDomain.CurrentDomain.RelativeSearchPath;
            string binPath = relativeSearchPath == null ? baseDir : Path.Combine(baseDir, relativeSearchPath);

            string templatesPathFull = Path.Combine(binPath, _TemplatePath);

            string fileName = string.Format("{0}/{1}.{2}", templatesPathFull, codeTemplate, _ExtensionFileTemplate);

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(string.Format(Resource.TemplateEngine_FileRepository_Not_Found, fileName));
            }

            TextReader reader = File.OpenText(fileName);

            string contentTemplate = reader.ReadToEnd();

            return contentTemplate;
        }

        
    }
}
