using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Mecanismo para contar palabras
    /// </summary>
    public interface IWordCounting
    {
        /// <summary>
        /// Contar palabras de un texto
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        int CountWords(string text);
    }
}
