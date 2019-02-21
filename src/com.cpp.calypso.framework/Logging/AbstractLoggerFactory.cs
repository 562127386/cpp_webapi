using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace com.cpp.calypso.framework
{
    public abstract class AbstractLoggerFactory : MarshalByRefObject, ILoggerFactory
    {
        public virtual ILogger Create(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            return Create(type.FullName);
        }

        public virtual ILogger Create(Type type, LoggerLevel level)
        {
            if (type == null) throw new ArgumentNullException("type");

            return Create(type.FullName, level);
        }

        public abstract ILogger Create(String name);

        public abstract ILogger Create(String name, LoggerLevel level);

        /// <summary>
        /// Gets the configuration file.
        /// </summary>
        /// <param name="fileName">i.e. log4net.config</param>
        /// <returns></returns>
        protected static FileInfo GetConfigFile(string fileName)
        {
            FileInfo result;

            if (Path.IsPathRooted(fileName))
            {
                result = new FileInfo(fileName);
            }
            else
            {
                result = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName));
            }

            return result;
        }
    }
}
