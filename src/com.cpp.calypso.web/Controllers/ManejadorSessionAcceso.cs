using System;
using System.Web;
using com.cpp.calypso.framework;
using com.cpp.calypso.framework.Session;

namespace com.cpp.calypso.web
{
    /// <summary>
    /// Manejador de Sesion para las acciones del controller Acceso
    /// </summary>
    public class ManejadorSessionAcceso : ISessionManager
    {
        #region <ENUMERABLE>
        public enum EnumerableSessionAcceso
        {
            /// <summary>
            /// Identificador del usuario, que paso la autentificacion (usuario y contrase;a) correcta
            /// </summary>
            usuario_id_pasado_autentificacion,
            
        }
        #endregion


        #region <METODOS>

        /// <summary>
        ///   Devuelve el valor de session de acuerdo al enumerable
        /// </summary>
        /// <param name = "sessionEnum">EnumerableSessionAcceso</param>
        /// <returns></returns>
        public static object getSession(EnumerableSessionAcceso sessionEnum)
        {
            var session = string.Format("{0}_{1}", typeof(ManejadorSessionAcceso).Name, sessionEnum.ToString());
            if (Enum.IsDefined(typeof(EnumerableSessionAcceso), sessionEnum))
            {
                return HttpContext.Current.Session[session];
            }
            throw new SessionException();
        }

        /// <summary>
        ///   Devuelve el valor de session de acuerdo al enumerable
        /// </summary>
        /// <param name = "sessionEnum">EnumerableSessionAcceso</param>
        /// <param name = "value">Valor a ser guardado en la Sesion</param>
        /// <returns></returns>
        public static void setSession(EnumerableSessionAcceso sessionEnum, object value)
        {
            if (Enum.IsDefined(typeof(EnumerableSessionAcceso), sessionEnum))
            {
                var session = string.Format("{0}_{1}", typeof(ManejadorSessionAcceso).Name, sessionEnum.ToString());
                HttpContext.Current.Session[session] = value;
            }
            else
            {
                throw new SessionException();
            }
        }

        /// <summary>
        ///   Elimina todos los valores de session en este control
        /// </summary>
        public static void Reset()
        {
            foreach (EnumerableSessionAcceso enumsession in Enum.GetValues(typeof(EnumerableSessionAcceso)))
            {
                var session = string.Format("{0}_{1}", typeof(ManejadorSessionAcceso).Name, enumsession.ToString());
                HttpContext.Current.Session.Remove(session);
            }
        }

        #endregion
    }
}
