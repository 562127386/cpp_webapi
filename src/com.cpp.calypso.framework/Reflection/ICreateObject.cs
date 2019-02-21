namespace com.cpp.calypso.framework
{
    /// <summary>
    /// Interfaz para crear objetos por reflexión
    /// </summary>
    public interface ICreateObject
    {
        /// <summary>
        /// Crear una instancia de objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T CreateInstance<T>();
 
    }

    
}
