using System;
using System.Collections.Generic;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Clase para realizar un tratamiento de excepciones en base a EnterpriseLibrary
    /// </summary>
    public class ManejadorExcepciones : IHandlerExcepciones
    {
        /// <summary>
        /// Variable paraa realizar Log
        /// </summary>
        static  ILogger log = null;

        //    ServiceLocator.Current.GetInstance<ILoggerFactory>().Create(typeof(ManejadorExcepciones));

        /// <summary>
        /// Lista de filtros aplicado 
        /// </summary>
        readonly List<IFilterHandleException> _filterHanderException;

        Exception _exception;
        Exception _innerException;

        string _mensaje = string.Empty;

        TypeResult _typeResultado = TypeResult.Error;

        public ManejadorExcepciones(IFilterHandleException filterHanderException, ILoggerFactory loggerFactory)
        {
            _filterHanderException = new List<IFilterHandleException>();

            //Default
            _filterHanderException.Add(new ApplyFriendlyMessageIFilterHandleException());

            //Personalizado
            _filterHanderException.Add(filterHanderException);

            ManejadorExcepciones.log = loggerFactory.Create(typeof(ManejadorExcepciones));
        }

        ///<summary>
        /// TODO:Existe inconveniente para enviar una lista de dependencias con Castle.Windsor
        ///</summary>
        ///<param name="filterHanderException"></param>
        //public ManejadorExcepciones(List<IFilterHandleException> filterHanderException, ILoggerFactory loggerFactory)
        //{
        //    _filterHanderException = filterHanderException;

        //    ManejadorExcepciones.log = loggerFactory.Create(typeof(ManejadorExcepciones));
        //}

        /// <summary>
        /// Indica el tipo de error producido
        /// </summary>
        public TypeResult TypeResultado
        {
            get { return _typeResultado; }
        }

        #region IManejadorExcepciones Members

        /// <summary>
        ///   Mensaje asociado al tipo de excepción
        /// </summary>
        public string Message
        {
            get { return _mensaje; }
        }

        /// <summary>
        /// Excepcion original interna
        /// </summary>
        public Exception InnerException
        {
            get { return _innerException; }
        }

        /// <summary>
        /// Excepcion resultado despues del tratamiento de excepciones
        /// </summary>
        public Exception Exception
        {
            get { return _exception; }
        }


        /// <summary>
        /// Manejar excepcion
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public HandleExceptionResult HandleException(Exception exception)
        {
            try
            {
                var result = new HandleExceptionResult();
                result.TypeResult = TypeResult.Error;
                result.Message =  Mensajes.ErrorGenerico;

                //Aplicar tuberia de filtros para el tratamiento de errores, basado en la configuraciones
                foreach (var filter in _filterHanderException)
                    result = filter.HandleException(exception, result);

                _mensaje = result.Message;
                _typeResultado = result.TypeResult;
                
				//Registrar la excepcion en log
                log.Error("Excepcion:", exception);


                return result;
            }
            catch (Exception ex)
            {
                if (log != null)
                {
                    log.Error("Excepcion en ManejadorExcepciones - Exception Original :", exception);

                    //Registrar la excepcion en NLog
                    log.Error("Excepcion en ManejadorExcepciones :", ex);
                }
            }
            return null;
        }

        #endregion
    }
}
