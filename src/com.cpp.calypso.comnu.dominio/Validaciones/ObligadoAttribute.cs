using System.ComponentModel.DataAnnotations;

namespace com.cpp.calypso.comun.dominio
{
    public class ObligadoAttribute : RequiredAttribute //, IClientValidatable
    {
        public ObligadoAttribute()
        {
            ErrorMessageResourceType = typeof (Mensajes);
            ErrorMessageResourceName = "RequiredAttribute_ValidationError";
        }

        //IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        //{
        //    string msg = FormatErrorMessage(metadata.GetDisplayName());
        //    yield return new ModelClientValidationRequiredRule(msg);
        //}
    }
}