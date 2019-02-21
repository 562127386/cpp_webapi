using System.Threading.Tasks;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Estereotipo: Interfase
    /// Responsabilidad: Define el método para procesar una plantilla con su modelo
    /// </summary>
    public interface ITemplateEngine
    {
        /// <summary>
        /// Procesa una plantilla y un modelo
        /// </summary>
        /// <param name="keyTemplate">Clave de la Plantilla</param>
        /// <param name="template">Plantilla</param>
        /// <param name="model">Objeto Modelo</param>
        /// <exception cref="IncorrectTemplateException"></exception>
        /// <returns></returns>
        Task<string> Process(string keyTemplate, string template, object model);


        Task<string> Process(string keyTemplate,  object model);
    }
}
