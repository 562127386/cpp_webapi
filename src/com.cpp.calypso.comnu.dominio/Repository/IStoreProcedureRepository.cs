using System;
using System.Collections;
using System.Collections.Generic;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Interfaz de informacion de un usuario
    /// </summary>
    public interface IStoreProcedureRepository<T> where T : class 
    {
        IList<T> SpConResultados(string comando, IEnumerable parametros);

        void SpConParametrosSalida(string comando, IEnumerable parametros);

        void SpConsola(string comando, IEnumerable parametros);

    }
}