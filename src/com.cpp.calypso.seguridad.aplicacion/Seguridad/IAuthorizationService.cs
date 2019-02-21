using com.cpp.calypso.comun.dominio;
using System.Threading.Tasks;

namespace com.cpp.calypso.seguridad.aplicacion
{
    /// <summary>
    /// Define una interface para autorizar el acceso de un usuario
    /// </summary>
    public interface IAuthorizationService 
    {
        /// <summary>
        /// Verificar si el usuario enviado como parametro, tiene permiso para realizar una accion en el codigo de la funcionalidad pasado
        /// </summary>
        /// <param name="usuarioInfo"></param>
        /// <param name="funcionalidadCodigo"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        Task<bool> Authorize(string funcionalidadCodigo, string actionCodigo);


        /// <summary>
        /// Verificar si el usuario actual, tiene permiso para realizar una accion en un recurso (Tipo de clase)
        /// </summary>
        /// <param name="action">Accion de la funcionaldiad a verificar</param>
        /// <returns></returns>
        Task<bool> Authorize(Accion action);
 
         
    }
}
