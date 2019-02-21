using System;

namespace com.cpp.calypso.framework
{
    public abstract class AbstractLogAuditoriaFactory : ILogAuditoriaFactory
    {
        public virtual ILogAuditoria Create(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            return Create(type.FullName);
        }

        public ILogAuditoria Create(Type type, LoggerLevel level)
        {
            if (type == null) throw new ArgumentNullException("type");

            return Create(type.FullName, level);
        }

        public abstract ILogAuditoria Create(string name);

        public abstract ILogAuditoria Create(string name, LoggerLevel level);
    }
}
