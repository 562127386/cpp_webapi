using Castle.Core.Logging;
using Castle.Facilities.Logging;
using System;

namespace com.cpp.calypso.web.Util
{

    public static class LoggingFacilityExtensions
    {
        public static LoggingFacility UseCustomNLog(this LoggingFacility loggingFacility)
        {
            ///return loggingFacility.LogUsing<CastleNLogLoggerFactory>().WithAppConfig();

            var factory = new CastleNLogLoggerFactory(new NLogFactory());
            return loggingFacility.LogUsing(factory).WithAppConfig();
        }
    }

    /// <summary>
    /// Unificar logs de castle y nlog,  con dependencias en las capas
    /// </summary>
    public class CastleNLogLoggerFactory : AbstractLoggerFactory
    {
        private readonly NLogFactory nLogFactory;

        public CastleNLogLoggerFactory(NLogFactory nLogFactory) 
        {
            this.nLogFactory = nLogFactory;
        }

         

        public override ILogger Create(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new CastleNlogLogger(nLogFactory.Create(name), this);
        }

        public override ILogger Create(string name, LoggerLevel level)
        {
            throw new NotSupportedException("Logger levels cannot be set at runtime. Please review your configuration file.");
        }
    }

    [Serializable]
    public class CastleNlogLogger :
        MarshalByRefObject,
        ILogger
    {
        private static readonly Type DeclaringType = typeof(CastleNlogLogger);

        public CastleNlogLogger(framework.ILogger logger, CastleNLogLoggerFactory factory)
        {
            Logger = logger;
            Factory = factory;
        }

        internal CastleNlogLogger()
        {
        }

     

        public bool IsDebugEnabled
        {
            get { return Logger.IsErrorEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return Logger.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return Logger.IsFatalEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return Logger.IsInfoEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return Logger.IsWarnEnabled; }
        }

        protected internal CastleNLogLoggerFactory Factory { get; set; }

        protected internal framework.ILogger Logger { get; set; }

        public override string ToString()
        {
            return Logger.ToString();
        }

        public virtual global::Castle.Core.Logging.ILogger CreateChildLogger(string name)
        {
            //TODO: 
            return Factory.Create( "ChildLogger." + name);
        }

        public void Debug(string message)
        {
            if (IsDebugEnabled)
            {
                Logger.Debug(message);
            }
        }

        public void Debug(Func<string> messageFactory)
        {
            if (IsDebugEnabled)
            {
                Logger.Debug(messageFactory.Invoke(), null);
            }
        }

        public void Debug(string message, Exception exception)
        {
            if (IsDebugEnabled)
            {
                Logger.Debug(message, exception);
            }
        }

        public void DebugFormat(string format, params Object[] args)
        {
            if (IsDebugEnabled)
            {
                //Logger.DebugFormat(format, new SystemStringFormat(CultureInfo.InvariantCulture, format, args));
                Logger.DebugFormat(format, args);
            }
        }

        public void DebugFormat(Exception exception, string format, params Object[] args)
        {
            if (IsDebugEnabled)
            {
                Logger.DebugFormat(exception,format, args);
            }
        }

        public void DebugFormat(IFormatProvider formatProvider, string format, params Object[] args)
        {
            if (IsDebugEnabled)
            {
                //TODO: Revision
                Logger.DebugFormat(format.ToString(formatProvider), args);
            }
        }

        public void DebugFormat(Exception exception, IFormatProvider formatProvider, string format, params Object[] args)
        {
            if (IsDebugEnabled)
            {
                //TODO: Revision
                Logger.DebugFormat(exception,format.ToString(formatProvider), args);
            }
        }

        public void Error(string message)
        {
            if (IsErrorEnabled)
            {
                Logger.Error(message);
            }
        }

        public void Error(Func<string> messageFactory)
        {
            if (IsErrorEnabled)
            {
                Logger.Error(messageFactory.Invoke());
            }
        }

        public void Error(string message, Exception exception)
        {
            if (IsErrorEnabled)
            {
                Logger.Error(message, exception);
            }
        }

        public void ErrorFormat(string format, params Object[] args)
        {
            if (IsErrorEnabled)
            {
                Logger.ErrorFormat(format, args);
            }
        }

        public void ErrorFormat(Exception exception, string format, params Object[] args)
        {
            if (IsErrorEnabled)
            {
                Logger.ErrorFormat(exception,format, args);
            }
        }

        public void ErrorFormat(IFormatProvider formatProvider, string format, params Object[] args)
        {
            if (IsErrorEnabled)
            {
                //TODO: Revision
                Logger.ErrorFormat(format.ToString(formatProvider), args);
            }
        }

        public void ErrorFormat(Exception exception, IFormatProvider formatProvider, string format, params Object[] args)
        {
            if (IsErrorEnabled)
            {
                Logger.ErrorFormat(exception,format.ToString(formatProvider), args);
            }
        }

        public void Fatal(string message)
        {
            if (IsFatalEnabled)
            {
                Logger.Fatal(message);
            }
        }

        public void Fatal(Func<string> messageFactory)
        {
            if (IsFatalEnabled)
            {
                Logger.Fatal(messageFactory.Invoke());
            }
        }

        public void Fatal(string message, Exception exception)
        {
            if (IsFatalEnabled)
            {
                Logger.Fatal(message, exception);
            }
        }

        public void FatalFormat(string format, params Object[] args)
        {
            if (IsFatalEnabled)
            {
                Logger.FatalFormat(format, args);
            }
        }

        public void FatalFormat(Exception exception, string format, params Object[] args)
        {
            if (IsFatalEnabled)
            {
                Logger.FatalFormat(exception,format, args);
            }
        }

        public void FatalFormat(IFormatProvider formatProvider, string format, params Object[] args)
        {
            if (IsFatalEnabled)
            {
                //TODO: Revision
                Logger.FatalFormat(format.ToString(formatProvider), args);
            }
        }

        public void FatalFormat(Exception exception, IFormatProvider formatProvider, string format, params Object[] args)
        {
            if (IsFatalEnabled)
            {
                //TODO: Revision
                Logger.FatalFormat(exception,format.ToString(formatProvider), args);
            }
        }

        public void Info(string message)
        {
            if (IsInfoEnabled)
            {
                Logger.Info(message);
            }
        }

        public void Info(Func<string> messageFactory)
        {
            if (IsInfoEnabled)
            {
                Logger.Info(messageFactory.Invoke());
            }
        }

        public void Info(string message, Exception exception)
        {
            if (IsInfoEnabled)
            {
                Logger.Info(message, exception);
            }
        }

        public void InfoFormat(string format, params Object[] args)
        {
            if (IsInfoEnabled)
            {
                Logger.InfoFormat(format, args);
            }
        }

        public void InfoFormat(Exception exception, string format, params Object[] args)
        {
            if (IsInfoEnabled)
            {
                Logger.InfoFormat(exception, format, args);
            }
        }

        public void InfoFormat(IFormatProvider formatProvider, string format, params Object[] args)
        {
            if (IsInfoEnabled)
            {
                //TODO: Revision
                Logger.InfoFormat(format.ToString(formatProvider), args);
            }
        }

        public void InfoFormat(Exception exception, IFormatProvider formatProvider, string format, params Object[] args)
        {
            if (IsInfoEnabled)
            {
                //TODO: Revision
                Logger.InfoFormat(exception, format.ToString(formatProvider), args);
            }
        }

        public void Warn(string message)
        {
            if (IsWarnEnabled)
            {
                Logger.Warn(message);
            }
        }

        public void Warn(Func<string> messageFactory)
        {
            if (IsWarnEnabled)
            {
                Logger.Warn(messageFactory.Invoke());
            }
        }

        public void Warn(string message, Exception exception)
        {
            if (IsWarnEnabled)
            {
                Logger.Warn(message, exception);
            }
        }

        public void WarnFormat(string format, params Object[] args)
        {
            if (IsWarnEnabled)
            {
                Logger.WarnFormat(format, args);
            }
        }

        public void WarnFormat(Exception exception, string format, params Object[] args)
        {
            if (IsWarnEnabled)
            {
                Logger.WarnFormat(exception,format, args);
            }
        }

        public void WarnFormat(IFormatProvider formatProvider, string format, params Object[] args)
        {
            if (IsWarnEnabled)
            {
                //TODO: Revision
                Logger.WarnFormat(format.ToString(formatProvider), args);
            }
        }

        public void WarnFormat(Exception exception, IFormatProvider formatProvider, string format, params Object[] args)
        {
            if (IsWarnEnabled)
            {
                //TODO: Revision
                Logger.WarnFormat(exception, format.ToString(formatProvider), args);
            }
        }
    }



}