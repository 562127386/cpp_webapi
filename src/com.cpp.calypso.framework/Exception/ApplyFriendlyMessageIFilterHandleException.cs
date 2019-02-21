using Abp.Runtime.Validation;
using com.cpp.calypso.comun.dominio;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace com.cpp.calypso.framework
{

    /// <summary>
    /// Aplicar un mensaje amigable para visualizar al usaurio, basado en varios criterios
    /// </summary>

    public class ApplyFriendlyMessageIFilterHandleException : IFilterHandleException
    {
        public HandleExceptionResult HandleException(Exception originalException, HandleExceptionResult result)
        {
            //TODO: JSA, APLICAR CADENA DE RESPONSABILIDAD
            Exception _innerException = originalException;


            if (_innerException.GetType() == typeof(GenericException))
            {
                var genericException = _innerException as GenericException;
                result.TypeResult = TypeResult.Information;
                result.Message = genericException.FriendlyMessage;
                return result;
            }


            if (_innerException.GetType() == typeof(AbpValidationException))
            {
                var genericException = _innerException as AbpValidationException;
                result.TypeResult = TypeResult.Information;

                var msg = string.Join("; ", genericException.ValidationErrors
                          .Select(x => x.ErrorMessage));

                result.Message = msg;
                return result;
            }


            if (_innerException.GetType() == typeof(ConcurrenciaExcepcion))
            {
                result.TypeResult = TypeResult.Error;
                result.Message = Mensajes.ErrorConcurrencia;
                return result;
            }

            //TODO: Colocar DbEntityValidationException, crea dependencia de EntityFramework
            if (_innerException.GetType() == typeof(DbEntityValidationException))
            {
                var dbEntityValidationException = _innerException as DbEntityValidationException;

                var fullErrorMessage = dbEntityValidationException.DbEntityValidationExceptionToString();

                result.TypeResult = TypeResult.Error;
                result.Message = fullErrorMessage;
                return result;
            }

            //TODO: Mensajes de recursos en el framework
            //Default 
            result.TypeResult = TypeResult.Error;
            result.Message = Mensajes.ErrorGenerico;
            return result;
        }

    }
}
