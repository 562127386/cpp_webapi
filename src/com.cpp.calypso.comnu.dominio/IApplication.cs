using System;

namespace com.cpp.calypso.comun.dominio
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
        UsuarioAutentificado GetCurrentUser();

        /// <summary>
        /// Establecer el usuario actual
        /// </summary>
        /// <param name="usuario"></param>
        void SetCurrentUser(UsuarioAutentificado usuario);
 
        /// <summary>
        /// Establcer el modulo autentificado
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <param name="modulo"></param>
        void SetCurrentModule(ModuloAutentificado modulo);

        /// <summary>
        /// Obtener el modulo autentificado
        /// </summary>
        /// <returns></returns>
        ModuloAutentificado GetCurrentModule();
    }
}
