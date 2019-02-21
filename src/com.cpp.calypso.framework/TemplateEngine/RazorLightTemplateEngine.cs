using com.cpp.calypso.comun.dominio;
using RazorLight;
using RazorLight.Caching;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Gestion de plantillas. Utilizando  RazorLight https://github.com/toddams/RazorLight
    /// </summary>
    public class RazorLightTemplateEngine : ITemplateEngine
    {
        private RazorLightEngine engine;

        public RazorLightTemplateEngine(string templatePath)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string relativeSearchPath = AppDomain.CurrentDomain.RelativeSearchPath;
            string binPath = relativeSearchPath == null ? baseDir : Path.Combine(baseDir, relativeSearchPath);

            string templatesPathFull = Path.Combine(binPath, templatePath);

            engine = new RazorLightEngineBuilder()
                .UseFilesystemProject(templatesPathFull)
              .UseMemoryCachingProvider()
              .Build();
        }
        
        public async Task<string> Process(string keyTemplate, string template, object model)
        {
            var cacheResult = engine.TemplateCache.RetrieveTemplate(keyTemplate);
            if (cacheResult.Success)
            {
                return await engine.RenderTemplateAsync(cacheResult.Template.TemplatePageFactory(), model);
            }

            return await engine.CompileRenderAsync(keyTemplate, template, model);

        }

        public async Task<string> Process(string keyTemplate, object model)
        {
            var cacheResult = engine.TemplateCache.RetrieveTemplate(keyTemplate);
            if (cacheResult.Success)
            {
                return await engine.RenderTemplateAsync(cacheResult.Template.TemplatePageFactory(), model);
            }

            return await engine.CompileRenderAsync(keyTemplate, model);
        }
    }
}
