using com.cpp.calypso.comun.dominio;
using Microsoft.CSharp.RuntimeBinder;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Responsabilidad: Implementa método para cargar plantilla y modelo
    /// </summary>
    public class RazorTemplateEngine : ITemplateEngine
    {
        private readonly IRepositoryTemplates repositoryTemplates;

        public RazorTemplateEngine()
        {
        }

        public RazorTemplateEngine(IRepositoryTemplates repositoryTemplates)
        {
            this.repositoryTemplates = repositoryTemplates;
        }


        #region ITemplateEngine Members

        /// <summary>
        /// Procesa una plantilla y un modelo
        /// </summary>
        /// <param name="keyTemplate">Clave de la Plantilla</param>
        /// <param name="template">Plantilla</param>
        /// <param name="model">Objeto Modelo</param>
        /// <exception cref="IncorrectTemplateException"></exception>
        /// <returns></returns>
        public async Task<string> Process(string keyTemplate, string template, object model)
        {
            return await Task.Run(() =>
            {

                if (string.IsNullOrEmpty(template)) throw new ArgumentNullException("template");
                if (model == null) throw new NullReferenceException();
                try
                {
                    InitializarRazorParser();

                    // return Razor.Parse(template, model as DynamicObject);

                    var result = Engine.Razor.RunCompile(template, keyTemplate, null, model);
                    return result;

                }
                catch (RuntimeBinderException ex)
                {
                    throw new IncorrectTemplateException(ex.Message + ex.StackTrace);
                }
                catch (TemplateCompilationException ex)
                {
                    throw new IncorrectTemplateException(ex.Message + ex.StackTrace);
                }
            });
        }

        public async Task<string> Process(string keyTemplate, object model)
        {
            if (repositoryTemplates != null) {
                var template = repositoryTemplates.GetContentTemplate(keyTemplate);
                return await Process(keyTemplate, template, model);
            }

            throw new NotSupportedException("No existe instancia de IRepositoryTemplates, para recuperar el contenido de la plantila, segun el codigo pasado");
        }


        #endregion

        void InitializarRazorParser()
        {
            // HACK: this is required to get the Razor Parser to work, no idea why, something to with dynamic objects i guess, tracked this down as the test worked sometimes, turned out
            // it was when the ViewBag was touched from the controller tests, if that happened before the Razor.Parse in ShoudSpikeTheSillyError() then it ran fine.
            dynamic x2 = new ExpandoObject();
            x2.Dummy = "";
        }
    }
}
