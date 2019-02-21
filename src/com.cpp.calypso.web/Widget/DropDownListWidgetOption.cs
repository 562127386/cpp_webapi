namespace com.cpp.calypso.web
{
    public class DropDownListWidgetOption : IWidgetOption
    {

        /// <summary>
        /// Nombre de Propiedad de Referencia. Dicha propiedad sera la que se utilice
        /// para enlace el valor seleccionado desde el Combo.
        /// 
        /// La referencia, debe ser el nombre de una propiedad que existan en la entidad. Caso contrario sera lanzado una exception
        /// </summary>
        public string PropertyRef { get; set; }


    }
}