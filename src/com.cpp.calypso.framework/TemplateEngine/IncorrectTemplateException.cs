using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Estereotipo: Excepcion Template
    /// Responsabilidad: Devolver una excepcion cuando en la plantilla no existen propiedades que estan en el modelo que se pasa 
    /// </summary>
    [Serializable]
    public class IncorrectTemplateException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">Mensaje</param>
        public IncorrectTemplateException(string message)
            : base(message) //, "Plantilla no encontrada")
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public IncorrectTemplateException()
        {
            //FriendlyMessage = Recursos.PlantillaNoEncontrada;
        }
    }
}
