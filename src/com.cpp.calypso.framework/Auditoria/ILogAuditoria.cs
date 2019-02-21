using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.framework
{
    public interface ILogAuditoria
    {

        void Write(string accion, string mensaje);
    }
}
