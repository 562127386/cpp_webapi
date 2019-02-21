using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.cpp.calypso.web
{
    public class ConstantesConfiguraciones
    {
        public const string CLAVE_CONFIGURACION_CODIGO_SISTEMA = "SISTEMA.NUCLEO.CODIGO_SISTEMA";
        public const string CLAVE_CONFIGURACION_CODIGO_MENU_PRINCIPAL = "SISTEMA.NUCLEO.CODIGO_MENU_PRINCIPAL";


        /// <summary>
        /// Clave de configuracion de AppSettings, donde se encuentra el listado de acciones a omitir para aplicar autorizacion. Cadena con elementos separados por ,. Referencia "INICIO,VALIDAR"
        /// </summary>
        public const string CLAVE_CONFIGURACION_SEGURIDAD_AUTORIZACION_ACCIONES_OMITIR  = "SISTEMA.NUCLEO.SEGURIDAD.ACCIONES.OMITIR";

        /// <summary>
        /// Clave de configuracion de AppSettings, donde se encuentra el listado de controladores a omitir para aplicar autorizacion. Cadena con elementos separados por ,. Referencia "ERROR,INICIO,HOME"
        /// </summary>
        public const string CLAVE_CONFIGURACION_SEGURIDAD_AUTORIZACION_CONTROLADORES_OMITIR = "SISTEMA.NUCLEO.SEGURIDAD.CONTROLADORES.OMITIR";

        /// <summary>
        /// Listado de sinonimos de nombres de acciones de mvc, de las acciones de las funcionalidades CRUD (VIEW, EDIT, DELETE, CREATE) para aplicar autorizacion automatica segun el nombre de la accion. 
        /// Cadena en forma JSON,  Un objeto con propieades VIEW, EDIT, DELETE, CREATE, los valores de las propiedades son cadenas con sinominos separados con , 
        /// 
        /// Ejemplo:
        /// {'VIEW':'INDEX,GET,BUSCAR,OBTENER,LIST,BUSQUEDA,DETAILS','EDIT':'ACTUALIZAR,UPDATE','DELETE':'BORRAR,ELIMINAR','CREATE':'NEW,NUEVO'}
        /// 
        /// Referencia:
        /// 'VIEW':'INDEX,GET,BUSCAR,OBTENER,LIST,BUSQUEDA,DETAILS'
        /// 'EDIT':'ACTUALIZAR,UPDATE'
        /// 'DELETE':'BORRAR,ELIMINAR'
        /// 'CREATE':'NEW,NUEVO'
        /// </summary>
        public const string CLAVE_CONFIGURACION_SEGURIDAD_AUTORIZACION_ACCIONES_CRUD_SINOMINOS = "SISTEMA.NUCLEO.SEGURIDAD.ACCIONES.CRUD.SINOMINOS";
        
       
    }
}
