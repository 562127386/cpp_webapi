using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.framework
{
    ///<summary>
    /// Metodos de protección. Tales como la comprobación de argumentos nulos, argumentos vacios, etc. Si no cumple las condiciones se lanzará excepciones
    /// 
    /// Todo:
    /// Se puede estandarizar o utilizar Microsoft's Code Contracts coming out with .NET 4.0
    /// http://research.microsoft.com/en-us/projects/contracts/
    ///</summary>
    public static class Guard
    {
        /// <summary>
        /// Comprueba si un objeto es nulo, si es el caso lanza una excepcion
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        public static void AgainstArgumentNull(object argument, string argumentName)
        {
            if (argument == null)
                throw new ArgumentNullException(argumentName, string.Format("{0} cannot be null", argumentName));
        }

        public static void AgainstNullOrEmptyString(string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argument))
                throw new ArgumentException(
                    string.Format(
                        "Argument {0} must not be null or an empty string",
                        argumentName));
        }

        public static void AgainstWrongFormatString(string argument, string argumentName, string format)
        {
            if (!argument.IsMatch(format))
                throw new ArgumentException(
                    string.Format(
                        "Argument {0} does not meet the expected format",
                        argumentName));
        }

        /// <summary>
        /// Comprueba si un valor no sea menor a otro 
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="argumentName"></param>
        /// <param name="value"></param>
        public static void AgainstLessThanValue(int argument, string argumentName, int value)
        {
            if (argument < value)
                throw new ArgumentOutOfRangeException(argumentName,
                                                      string.Format("El valor usado para {0} no puede ser menor a {1}",
                                                                    argumentName, value));
        }
    }
}
