using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cpp.calypso.framework
{
    public class ClaveDuplicadaExcepcion : Exception
    {
        public ClaveDuplicadaExcepcion(string mensaje)
            :base(mensaje)
        {
        }
    }
}
