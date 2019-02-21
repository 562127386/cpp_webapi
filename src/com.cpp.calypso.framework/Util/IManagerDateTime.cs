using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Gestion de fechas y tiempos
    /// </summary>
    public interface IManagerDateTime
    {
        /// <summary>
        /// Obtener fecha y hora actual
        /// </summary>
        /// <returns></returns>
        DateTime Get();
    }
}
