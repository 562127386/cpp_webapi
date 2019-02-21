using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Manages the instantiation of <see cref="ILogger"/>s.
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// Creates a new logger, getting the logger name from the specified type.
        /// </summary>
        ILogger Create(Type type);

        /// <summary>
        /// Creates a new logger.
        /// </summary>
        ILogger Create(String name);

        /// <summary>
        /// Creates a new logger, getting the logger name from the specified type.
        /// </summary>
        ILogger Create(Type type, LoggerLevel level);

        /// <summary>
        /// Creates a new logger.
        /// </summary>
        ILogger Create(String name, LoggerLevel level);
    }
}
