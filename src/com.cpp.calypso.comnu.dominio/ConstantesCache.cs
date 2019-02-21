using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Constantes relacionados a la gestion de cache. 
    /// Utilizar jerarquias para definir las constantes de cache. 
    /// Cache.MODULO.CAPA.Nombre
    /// </summary>
    public static class ConstantesCache
    {
        
        public const string CACHE_FUNCIONALIDADES_SISTEMA = "Cache.Comun.Dominio.Funcionalidad";

        public const string CACHE_PARAMETROS_SISTEMA = "Cache.Comun.Dominio.Parametros";
 
        public const string CACHE_ROLES_SISTEMA = "Cache.Comun.Dominio.Roles";

        public const string CACHE_ROLES_DTO_SISTEMA = "Cache.Comun.Dominio.Roles.Dto";

        public const string CACHE_CATALOGOS_SISTEMA = "Cache.Comun.Dominio.Catalogos";

        public const string CACHE_MODULOS_SISTEMA = "Cache.Comun.Dominio.Modulos";
    }
}
