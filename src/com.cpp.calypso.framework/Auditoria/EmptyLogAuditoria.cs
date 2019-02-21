using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cpp.calypso.framework
{
    public class EmptyLogAuditoria : ILogAuditoria
    {
        public void Write(string accion, string mensaje)
        {
            //Empty
        }
    }
}
