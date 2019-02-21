using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Gestion de fechas y horas local
    /// </summary>
    public class LocalManagerDateTime : IManagerDateTime
    {
        /// <summary>
        ///  Obtener fecha y hora actual
        /// </summary>
        /// <returns></returns>
        public DateTime Get()
        {
            return DateTime.Now;
        }
    }
}
