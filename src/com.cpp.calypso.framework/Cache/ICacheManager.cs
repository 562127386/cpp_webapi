using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Define methods and properties for a cache manager
    /// </summary>
    public interface ICacheManager /*<T> where T: class */
    {

        /// <summary>
        /// Add an object to the cache on a specific group
        /// </summary>
        /// <param name="key">The key used to lookup for the object in the cache.</param>
        /// <param name="data">The object itself.</param>
        /// <param name="group">The name of the group</param>        
        /// <param name="expirationTime">The expiration time (in seconds). 0=> Significa que no expira</param>
        void Add(string key, object data, string group = null, int expirationTime = 0);


        /// <summary>
        /// Removes the object from the cache from a specific group
        /// </summary>
        /// <param name="key">The key used to lookup for the object in the cache.</param>
        /// <param name="group">The name of the group</param>
        void Remove(string key, string group = null);

        /// <summary>
        /// Eliminar el listado de claves de cache
        /// </summary>
        /// <param name="keys"></param>
        void RemoveAll(IEnumerable<string> keys);

        /// <summary>
        /// Determines if an ojects exists on the cache on a specific group.
        /// TODO: JSA, VER LA NECESIDAD DE TENER ESTE MECANISMO, YA QUE ALGUNOS PROVEEDORES NO IMPLMENTA ESTO
        /// Y ES MEJOR TRAER EL OBJETO, EN EL CASO DE SER NULO SE ASUME QUE NO EXISTE PERO SI NO ES YA SE LO PUEDE UTILIZAR Y NO SE VUELVE A TRAER
        /// YA QUE LOS SERVIDORES O PROCESOS DE CACHE PUEDE ESTAR FUERA DEL SERVIDOR DONDE SE CONSUME DICHO CACHE
        /// </summary>
        /// <param name="key">The key used to lookup for the object in the cache.</param>
        /// <param name="group">The name of the group.</param>
        /// <returns>True if the object is present on the cache, otherwise false</returns>
        bool Contains(string key, string group = null);

        /// <summary>
        /// Removes all cache items of a specific group
        /// </summary>
        /// <param name="group"></param>
        void Flush(string group = null);

        /// <summary>
        /// Get the object in the cache specified by the key and a group name
        /// </summary>
        /// <param name="key">The key used to lookup for the object in the cache.</param>
        /// <param name="group">The name of the ggroup.</param>
        /// <returns>The object stored in the cache with the specified key</returns>
        //T GetData(string key);
        object GetData(string key, string group = null);

        /// <summary>
        /// Get the total of items in the cache
        /// </summary>
        /// <returns>A number indicating the total of items</returns>
        long GetTotalOfItems(string group = null);
    }
}
