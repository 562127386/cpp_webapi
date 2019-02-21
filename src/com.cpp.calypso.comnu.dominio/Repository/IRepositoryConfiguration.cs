using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.comun.dominio
{
    // <summary>
    /// Configuracion de Repositorios. Configurar el ORM implementado
    /// </summary>
    public interface IRepositoryConfiguration
    {
        /// <summary>
        /// Metodo para realizar la configuracion del repositorio
        /// </summary>
        /// <param name="ConnectionString">Cadena de conexion a la base de datos</param>
        /// <param name="dbSchema">Esquema de la base de datos</param>
        void Configure(string ConnectionString, string dbSchema);
    }

}
