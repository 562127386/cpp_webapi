using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.framework
{

    /// <summary>
    /// Filtro para utilizar elmah para trata la exception. Visualizar de excepciones, notificaciones por correo, todo el potencial del elmah
    /// </summary>
    public class ElmahFilterHandleException : IFilterHandleException
    {
        #region IFilterHandleException Members

        public HandleExceptionResult HandleException(Exception originalException, HandleExceptionResult result)
        {
            //wrapper Elmah
            ElmahExtension.LogToElmah(originalException);

            return result;
        }

        #endregion
    }
}
