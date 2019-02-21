using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Reposositorio de grupos de claves de cache, para facilitar la administracion de cache. Se lo puede utilizar en los proveedores de cache que no posee agrupamiento de claves de cache
    /// </summary>
    public interface IRepositoryGroupCache
    {
        /// <summary>
        /// Adicionar una clave al grupo
        /// </summary>
        /// <param name="key"></param>
        /// <param name="group"></param>
        void Add(string key, string group);

        /// <summary>
        /// Recuperar todas las claves asociadas a un grupo
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        List<string> GetListKey(string group);
        /// <summary>
        /// Eliminar un grupo del repositorio
        /// </summary>
        /// <param name="group"></param>
        void RemoveGroup(string group);

        /// <summary>
        /// Elimina una clave del grupo
        /// </summary>
        /// <param name="key"></param>
        /// <param name="group"></param>
        void Remove(string key, string group);
    }
}
