using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Ejecutar SQL en la base de datos
    /// </summary>
    public interface ISqlExecuter
    {
      
        /// <summary>
        /// Ejecutar con sql [update,delete,create]
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int Execute(string sql, params object[] parameters);

        /// <summary>
        /// Ejecutar una consulta sql [select]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<T> SqlQuery<T>(string sql, params object[] parameters);
    }
}
