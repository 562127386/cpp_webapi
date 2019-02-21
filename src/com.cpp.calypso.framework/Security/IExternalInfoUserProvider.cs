using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.framework
{

    /// <summary>
    /// Interfaz para obtener información externa de un usuario
    /// </summary>
    public interface IExternalInfoUserProvider
    {
        /// <summary>
        /// Obtener información externa de un usuario
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        ExternalInfoUser GetAtributosForUser(string username);
    }
}
