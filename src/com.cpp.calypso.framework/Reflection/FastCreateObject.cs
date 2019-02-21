using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection.Emit;
using System.Reflection;

namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Creacion de Objetos utilizando metodo mejorado con Emit y Delegate
    /// </summary>
    public class FastCreateObject : ICreateObject
    {
        #region ICreateObject Members

        public T CreateInstance<T>()
        {
            var creator = FastObjectFactory.CreateObjectFactory<T>();
            var obj = creator() is T ? (T)creator() : default(T);
            return obj;
        }

        #endregion
    }

    /// <summary>
    /// Clase para realizar creación de objetos con reflexión con un mejor performance
    /// </summary>
    public static class FastObjectFactory
    {
        #region Delegates

        public delegate object CreateObject();

        #endregion

        static readonly Hashtable creatorCache = Hashtable.Synchronized(new Hashtable());
        static readonly Type coType = typeof(CreateObject);

        /// <summary>
        /// Clear cache
        /// </summary>
        public static void Clear()
        {
            lock (creatorCache.SyncRoot)
            {
                creatorCache.Clear();
            }
        }


        /// <summary>
        /// Create a new instance of the specified type
        /// </summary>
        /// <returns></returns>
        public static CreateObject CreateObjectFactory<T>() // where T : class
        {
            Type t = typeof(T);
            CreateObject c = creatorCache[t] as CreateObject;
            if (c == null)
            {
                lock (creatorCache.SyncRoot)
                {
                    c = creatorCache[t] as CreateObject;
                    if (c != null)
                    {
                        return c;
                    }
                    DynamicMethod dynMethod = new DynamicMethod("DM$OBJ_FACTORY_" + t.Name, typeof(object), null, t);
                    ILGenerator ilGen = dynMethod.GetILGenerator();

                    //ilGen.Emit(OpCodes.Newobj, t.GetConstructor(Type.EmptyTypes));
                    ilGen.Emit(OpCodes.Newobj, t.GetConstructor(BindingFlags.Public |
                                                                BindingFlags.NonPublic | BindingFlags.Instance, null,
                                                                Type.EmptyTypes, null));

                    ilGen.Emit(OpCodes.Ret);
                    c = (CreateObject)dynMethod.CreateDelegate(coType);
                    creatorCache.Add(t, c);
                }
            }
            return c;
        }
    }
}
