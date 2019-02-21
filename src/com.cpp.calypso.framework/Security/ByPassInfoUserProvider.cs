using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Implementacion que salta extraer informacion del usuario desde una fuente externa 
    /// </summary>
    public class ByPassInfoUserProvider : IExternalInfoUserProvider
    {
        /// <summary>
        /// Retorna siempre un valor nulo
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public ExternalInfoUser GetAtributosForUser(string username)
        {
            return null;
        }
    }
}
