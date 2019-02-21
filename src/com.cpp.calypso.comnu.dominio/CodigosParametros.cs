using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace com.cpp.calypso.comun.dominio
{
    public static class CodigosParametros
    {


        public const string CODIGO_CLAVE_UNICA_USUARIO_CUENTA = "UX_USR_CUENTA";


        #region <Gestion de Usuarios, Roles>


        public const string PARAMETRO_SEGURIDAD_UTILIZAR_ROLES_EXTERNOS = "U_ROL_EXTERNOS";
        public const string PARAMETRO_SEGURIDAD_SINCRONIZAR_USUARIOS_EXTERNOS = "U_SYNC_USER";


        #endregion



        public const string PARAMETRO_NOMBRE_ORGANIZACION = "N_ORGANIZACION";
        

         #region <Gestion Interfaz del usuario>

         public const string PARAMETRO_PLANTILLA_SITIO = "N_THEME";

        public const string PARAMETRO_TAMAÑO_PAGINA_GRILLAS = "UI.PAGE_SIZE";

        #endregion




        public const string PARAMETRO_NOMBRE_APLICACION = "UI.NOMBRE_APLICACION";

        /// <summary>
        /// Recolectar informacion (Ejemplo Google analytics)
        /// </summary>
        public const string PARAMETRO_RECOLECTAR_INFO_ANALISIS = "UI.RECOLECTAR_INFO_ANALISIS";

        
        /// <summary>
        /// Parametro para establecer la carpeta para guardar imagenes 
        /// </summary>
        public const string PARAMETRO_IMAGENES_CARPETA = "DATA.ARCHIVOS.IMAGENES.CARPETA";

        /// <summary>
        /// Parametro para establecer la URL base para visualizacion de imagenes
        /// </summary>
        public const string PARAMETRO_IMAGENES_URL_BASE = "DATA.ARCHIVOS.IMAGENES.URL";

        /// <summary>
        /// Logo de la aplicacion
        /// </summary>
        public const string PARAMETRO_APLICACION_LOGO = "UI.APLICACION.LOGO";

        /// <summary>
        /// La url base del sistema
        /// </summary>
        public static string PARAMETRO_SISTEMA_URL = "SISTEMA.URL";
        


        #region <Configuraciones de Seguridad>


        /// <summary>
        /// X-Frame-Options. Aplicar restricciones para no permitir ember el sitio en otros sitios
        /// </summary>
        public const string PARAMETRO_SEGURIDAD_X_FRAME = "SEGURIDAD.X-FRAME";

        /// <summary>
        /// Eliminar cabeceras X-AspNetMvc, X-Powered, X-AspNet-Version
        /// </summary>
        public const string PARAMETRO_DELETE_HEADER_ASP_NET = "UI.DELETE_HEADER_ASP_NET";

        /// <summary>
        /// Tipo de bloque a un usuario (minutos), por intentos fallidos al autentificar
        /// </summary>
        public const string PARAMETRO_SEGURIDAD_BLOQUEO_USUARIO_TIEMPO = "SEGURIDAD.BLOQUEO_USUARIO.TIEMPO";

        /// <summary>
        /// Cantidad de intentos fallidos que se permiten a un usuario, antes de realizar un bloqueo.
        /// </summary>
        public const string PARAMETRO_SEGURIDAD_BLOQUEO_USUARIO_INTENTOS = "SEGURIDAD.BLOQUEO_USUARIO.INTENTOS";


        public const string PARAMETRO_SEGURIDAD_CLAVE_MINIMO_LONGITUD = "SEGURIDAD.CLAVE.MINIMO";
        public const string PARAMETRO_SEGURIDAD_CLAVE_REQUIERE_CARACTER_DIFERENTE_LETRA_DIGITO = "SEGURIDAD.CLAVE.REQUIERE.DIFERENTE_DIGITO_LETRA";
        public const string PARAMETRO_SEGURIDAD_CLAVE_REQUIERE_DIGITO = "SEGURIDAD.CLAVE.REQUIERE.DIGITO";
        public const string PARAMETRO_SEGURIDAD_CLAVE_REQUIERE_LETRA_MINUSCULA = "SEGURIDAD.CLAVE.REQUIERE.LETRA_MINUSCULA";
        public const string PARAMETRO_SEGURIDAD_CLAVE_REQUIERE_LETRA_MAYUSCULA = "SEGURIDAD.CLAVE.REQUIERE.LETRA_MAYUSCULA";

        #endregion

    }
}