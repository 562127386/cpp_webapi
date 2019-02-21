using System;

namespace com.cpp.calypso.framework
{
    public interface ILogAuditoriaFactory
    {
        /// <summary>
        /// Creates a new logger, getting the logger name from the specified type.
        /// </summary>
        ILogAuditoria Create(Type type);

        /// <summary>
        /// Creates a new logger.
        /// </summary>
        ILogAuditoria Create(String name);

        /// <summary>
        /// Creates a new logger, getting the logger name from the specified type.
        /// </summary>
        ILogAuditoria Create(Type type, LoggerLevel level);

        /// <summary>
        /// Creates a new logger.
        /// </summary>
        ILogAuditoria Create(String name, LoggerLevel level);
    }
}
