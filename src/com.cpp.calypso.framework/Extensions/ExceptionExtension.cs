using System.Data.Entity.Validation;
using System.Linq;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Extensiones para trabajar con exceptiones
    /// </summary>
    public static class  ExceptionExtension
    {
        /// <summary>
        /// Generar una cadena, con la lista de errores de validacion existentes en DbEntityValidationException
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="sep"></param>
        /// <returns></returns>
        public static string DbEntityValidationExceptionToString(this DbEntityValidationException ex, string sep = "; ")
        {
            //TODO: Colocar DbEntityValidationException, crea dependencia de EntityFramework 
            var errorMessages = (from eve in ex.EntityValidationErrors
                                 let entity = eve.Entry.Entity.GetType().Name
                                 from ev in eve.ValidationErrors
                                 select new
                                 {
                                     Entity = entity,
                                     PropertyName = ev.PropertyName,
                                     ErrorMessage = ev.ErrorMessage
                                 });

            return string.Join(sep, errorMessages.Select(e => string.Format(Resource.ExceptionExtensionDbEntityValidationExceptionFormat, e.Entity, e.PropertyName, e.ErrorMessage)));

        }
    }
}
