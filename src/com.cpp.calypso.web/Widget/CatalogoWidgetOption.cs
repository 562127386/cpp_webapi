namespace com.cpp.calypso.web
{
    /// <summary>
    /// Opciones para el widget Catalogo. 
    /// 
    /// </summary>
    public class CatalogoWidgetOption: IWidgetOption
    {

        public CatalogoWidgetOption() {

            CampoClave = "Codigo";

        }
        /// <summary>
        /// Codigo del Catalogo
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Nombre del Campo del item de catalogo, para generar la clave 
        /// </summary>
        public string CampoClave { get; set; }

    }

    
}