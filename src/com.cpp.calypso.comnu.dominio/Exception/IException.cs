using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    ///   Estereotipo: Interface
    ///   Responsabilidad: Define métodos y propiedades para construir excepciones personalizadas
    /// </summary>
    public interface IException
    {
        string FriendlyMessage { get; set; }
    }
}
