using System;

namespace com.cpp.calypso.framework
{

    /// <summary>
    /// Factoria, para crear Logger vacios. Es decir no realizan ningun registro de logs, unicamente permiten
    /// utilizar para resolver dependencias
    /// </summary>
    public class EmptyLoggerFactory : AbstractLoggerFactory
    {
        public override ILogger Create(string name)
        {
           return new EmptyLogger();
        }

        public override ILogger Create(string name, LoggerLevel level)
        {
            return new EmptyLogger();
        }
    }
}
