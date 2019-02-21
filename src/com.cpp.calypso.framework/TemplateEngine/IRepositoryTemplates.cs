namespace com.cpp.calypso.framework
{
    /// <summary>
    /// nterfaz que maneja el repositorio de templates
    /// </summary>
    public interface IRepositoryTemplates
    {
        /// <summary>
        /// Obtener el contenido del templates, por medio del codigo
        /// </summary>
        /// <param name="codeTemplate"></param>
        /// <returns></returns>
        string GetContentTemplate(string codeTemplate);
    }
}
