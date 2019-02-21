using System;
using System.Collections.Generic;

namespace com.cpp.calypso.seguridad.aplicacion
{
    /// <summary>
    /// Configuracion de auditoria
    /// </summary>
    public class AuditoriaConfiguracion
    {
        /// <summary>
        /// Nombre de entidad que se desea auditar
        /// </summary>
        public string Entidad { get; set; }

        /// <summary>
        /// Lista de propiedades de la entidad que se desea auditar
        /// </summary>
        public List<String> Propiedades { get; set; }

        public AuditoriaConfiguracion()
        {
            Propiedades = new List<string>();
        }

        public override string ToString()
        {
            return string.Format("Entidad: {0}. Propiedades: {1}", Entidad, string.Join(",", Propiedades));
        }
    }

}
