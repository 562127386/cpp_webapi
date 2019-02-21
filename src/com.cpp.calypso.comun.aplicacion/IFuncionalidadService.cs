using Abp.Application.Services;
using com.cpp.calypso.comun.dominio;
using System.Collections.Generic;
namespace com.cpp.calypso.comun.aplicacion
{
    /// <summary>
    /// Servicios para la gestion de funcionalidades
    /// </summary>
    public interface IFuncionalidadService :   IApplicationService
    {
        /// <summary>
        /// Obtener listado de funcionalidades
        /// </summary>
        /// <returns></returns>
        IEnumerable<Funcionalidad> GetFuncionalidades();
    }
}
