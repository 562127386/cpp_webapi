using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Filtros aplicados en una tuberia para procesar excepcion
    /// </summary>
    public interface IFilterHandleException
    {
        /// <summary>
        /// Publica mensaje con la excepcion
        /// </summary>
        /// <param name="originalException"></param>
        /// <param name="result"></param>
        HandleExceptionResult HandleException(Exception originalException, HandleExceptionResult result);
    }
}
