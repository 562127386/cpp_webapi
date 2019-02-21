using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.comun.dominio
{
    
    [Serializable]
    public class GenericException : Exception, IException
    {
        #region Implementation of IException

        public GenericException(string message, Exception innerException, string friendlyMessage)
            : base(message, innerException)
        {
            FriendlyMessage = friendlyMessage;
        }

        public string FriendlyMessage { get; set; }

        #endregion

        public GenericException(string message, string friendlyMessage)
            : base(message)
        {
            FriendlyMessage = friendlyMessage;
        }

        public GenericException()
        {
            FriendlyMessage = "Existe un inconveniente, por favor vuelva a intentar";
        }
    }
 
}
