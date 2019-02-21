using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Runtime.Caching;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Defines methods to manage  Cache.
    /// </summary>
    public class LocalCacheManager : ICacheManager /*<T> : ICacheManager<T> where T : class*/
    {

        /// <summary>
        /// Guardar los dao, que deben tener cache en una estructura mas optimizada
        /// </summary>
        private static ConcurrentDictionary<string, List<string>> _regions = new ConcurrentDictionary<string, List<string>>();


        private readonly ObjectCache cache;
        private readonly CacheItemPolicy policy;

        public LocalCacheManager()
        {
            cache = MemoryCache.Default;

            //TODO. Agregar politica de cache Manualmente. Estas politicas se pueden colocar a nivel de web.config??. Investigar
            policy = new CacheItemPolicy
            {
                //TODO: Posible desborde para el valor
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
            };
        }


        #region ICacheManager Members

        /// <summary>
        /// Add an object to the cache on a specific group
        /// </summary>
        /// <param name="key">The key used to lookup for the object in the cache.</param>
        /// <param name="data">The object itself.</param>
        /// <param name="group">The name of the group</param>        
        /// <param name="expirationTime">The expiration time (in seconds). 0=> Significa que no expira</param>
        public void Add(string key, object data, string group = null, int expirationTime = 0)
        {

            if (expirationTime != 0)
            {
                //Guard.AgainstLessThanValue(expirationTime, "expirationTime", 0);
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(expirationTime);
            }

            cache.Set(key, data, policy);
        }

        /// <summary>
        /// Removes the object from the cache from a specific group
        /// </summary>
        /// <param name="key">The key used to lookup for the object in the cache.</param>
        /// <param name="group">The name of the group</param>
        public void Remove(string key, string group = null)
        {
            cache.Remove(key);
        }

        /// <summary>
        /// Determines if an ojects exists on the cache on a specific group
        /// </summary>
        /// <param name="key">The key used to lookup for the object in the cache.</param>
        /// <param name="group">The name of the group.</param>
        /// <returns>True if the object is present on the cache, otherwise false</returns>
        public bool Contains(string key, string group = null)
        {
            return cache.Contains(key);
        }

        /// <summary>
        /// Removes all cache items of a specific group
        /// </summary>
        /// <param name="group"></param>
        public void Flush(string group = null)
        {
            if (string.IsNullOrEmpty(group))
            {
                InternalFlush();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Get the object in the cache specified by the key and a group name
        /// </summary>
        /// <param name="key">The key used to lookup for the object in the cache.</param>
        /// <param name="group">The name of the ggroup.</param>
        /// <returns>The object stored in the cache with the specified key</returns>
        //T GetData(string key);
        public object GetData(string key, string group = null)
        {
            CacheItem cacheItem = cache.GetCacheItem(key);
            if (cacheItem != null)
                return cacheItem.Value;
            return null;
        }

        /// <summary>
        /// Get the total of items in the cache
        /// </summary>
        /// <returns>A number indicating the total of items</returns>
        public long GetTotalOfItems(string group = null)
        {
            if (string.IsNullOrEmpty(group))
            {
                return cache.GetCount();
            }
            else
            {
                //TODO. JSA. COLOCAR LOS GRUPOS EN UNA VARIABLE COMPARTIDA ??
                //  static readonly Hashtable creatorCache = Hashtable.Synchronized(new Hashtable());
                throw new NotImplementedException();
            }
        }

        #endregion

        #region < Private Methods >

        /// <summary>
        /// Remove all the items on the cache
        /// </summary>
        private void InternalFlush()
        {

            List<string> keys = cache.Select(cacheItem => cacheItem.Key).ToList();

            foreach (string key in keys)
            {
                //Establecer una fecha de Expiration antigua.. 100 years.. 
                cache.Set(key, new object(), DateTimeOffset.Now.AddYears(100));
                cache.Remove(key);
            }
        }



        #endregion



        public void RemoveAll(IEnumerable<string> keys)
        {
            foreach (string key in keys)
            {

                cache.Set(key, new object(), DateTime.MinValue);
                cache.Remove(key);
            }
        }
    }
}
