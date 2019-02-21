using System.ComponentModel.DataAnnotations;


namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Determina la longitud masiva que cadena o matriz debe tener
    /// </summary>
    public class LongitudMayorAttribute : MaxLengthAttribute //, IClientValidatable
    {
        public LongitudMayorAttribute(int length)
            : base(length)
        {
            ErrorMessageResourceType = typeof(Mensajes);
            ErrorMessageResourceName = "MaxLengthAttribute_ValidationError";
        }

        //IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        //{
        //    string msg = FormatErrorMessage(metadata.GetDisplayName());
        //    yield return new ModelClientValidationMaxLengthRule(msg, Length);
        //}
    }
}