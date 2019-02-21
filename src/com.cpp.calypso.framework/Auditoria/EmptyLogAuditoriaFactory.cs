using System;

namespace com.cpp.calypso.framework
{
    public class EmptyLogAuditoriaFactory : AbstractLogAuditoriaFactory
    {
        public override ILogAuditoria Create(string name)
        {
           return new EmptyLogAuditoria();
        }

        public override ILogAuditoria Create(string name, LoggerLevel level)
        {
            return new EmptyLogAuditoria();
        }
    }
}
