using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.cpp.calypso.seguridad.aplicacion
{
    public interface IRolService : IAsyncBaseCrudAppService<Rol, RolDto, PagedAndFilteredResultRequestDto>
    {
    
        /// <summary>
        /// Actualiza los permisos de un rol. Crear Permisos, o eliminar permisos, segun el listado de identificadores de acciones enviados
        /// </summary>
        /// <param name="rol">Rol</param>
        /// <param name="listAccionId">Lista de acciones de funcionalidades que debe posee</param>
        /// <returns></returns>
        RolDto UpdatePermissions(int rolId,int[] listAccionId);

        /// <summary>
        /// Obtener un rol, por el codigo
        /// </summary>
        /// <param name="codigoRol"></param>
        /// <returns></returns>
        Task<RolDto> Get(string codigoRol);


        Task<RolPermisosDto> GetRolAndPermissions(int id);


        Task<IEnumerable<RolPermisosDto>> GetAllRolAndPermissions();


        Task<RolDetalleDto> GetDetalle(int rolId);


        Task<Tuple<bool, string>> CanRemoved(RolDto input);
    }
}
