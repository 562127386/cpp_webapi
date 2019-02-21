using System;

namespace com.cpp.calypso.seguridad.dominio
{
    /// <summary>
    /// Interfaz para obtener informacion relacionada a la aplicacion o sistema
    /// </summary>
    public interface IApplication
    {

        /// <summary>
        /// Obtener el fecha y tiempo 
        /// </summary>
        /// <returns></returns>
        DateTime getDateTime();

        /// <summary>
        /// Verificar si la aplicacion esta autentificada
        /// </summary>
        /// <returns></returns>
        bool IsAuthenticated();


        ///<summary>
        /// Obtiene la informacion del usuario autenticado
        ///</summary>
        ///<returns></returns>
        Usuario GetCurrentUser();

        /// <summary>
        /// Establecer el usuario actual
        /// </summary>
        /// <param name="usuario"></param>
        void SetCurrentUser(Usuario usuario);


        /// <summary>
        /// Obtiene la actual sesion de acceso del usuario
        /// </summary>
        /// <returns></returns>
        Sesion GetCurrentSession();



        /// <summary>
        /// Establece la sesion actual 
        /// </summary>
        /// <param name="sesion"></param>
        void SetCurrentSession(Sesion sesion);


        /// <summary>
        /// Obtener el actual Rol Autentificado
        /// </summary>
        /// <returns></returns>
        Rol GetCurrentRol();

        /// <summary>
        /// Establecer el rol autentificado
        /// </summary>
        /// <param name="sistema"></param>
        void SetCurrentRol(Rol rol);
 

    }
}
