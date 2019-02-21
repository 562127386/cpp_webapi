using System.ComponentModel.DataAnnotations;


namespace com.cpp.calypso.comun.dominio
{
    public class FechaAttribute: DataTypeAttribute
    {
        public FechaAttribute() : base(DataType.Date)
        {
            ErrorMessageResourceType = typeof (Mensajes);
            ErrorMessageResourceName =  "DataTypeAttribute_DateValidationError";
        }
    }
}