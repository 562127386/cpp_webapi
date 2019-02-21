using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cpp.calypso.framework
{
    public class ProcedimientoAlmacenadoExcepcion : Exception
    {
        public ProcedimientoAlmacenadoExcepcion(string mensaje,Exception exception)
            : base(mensaje, exception)
        {
        }
    }
}
