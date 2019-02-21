using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;

namespace com.cpp.calypso.seguridad.aplicacion
{
    /// <summary>
    /// Servicio de Aplicacion de Rol
    /// </summary>
    public class RolService : AsyncBaseCrudAppService<Rol,RolDto, PagedAndFilteredResultRequestDto>,
        IRolService
    {
        ICacheManager _cacheManager;
        IApplication _application;
        IBaseRepository<Permiso> _repositoryPermiso;
        private readonly IUsuarioRepository<Usuario> usuarioRepository;

        public RolService(
            IApplication application,
           IRolRepository<Rol> repository,
           IBaseRepository<Permiso> repositoryPermiso,
           IUsuarioRepository<Usuario> usuarioRepository,
           ICacheManager cacheManager)
           : base(repository)
        {
            _application = application;
            _repositoryPermiso = repositoryPermiso;
            this.usuarioRepository = usuarioRepository;
            _cacheManager = cacheManager;
        }

        public override async Task<IList<RolDto>> GetAll()
        {
            var roles = (await GetAllRolesAndPermissionsInternal()).Select(r => ObjectMapper.Map<RolDto>(r))
                .ToList();

            return roles;   
        }


        public async Task<RolDto> Get(string codigoRol)
        {
            var rol = (await GetAllRolesAndPermissionsInternal()).Where(r => r.Codigo == codigoRol).SingleOrDefault();

            return ObjectMapper.Map<RolDto>(rol);
        }


        public  async Task<RolDetalleDto> GetDetalle(int rolId)
        {

            var rolQuery = Repository.GetAll();
            var usuarioQuery = usuarioRepository.GetAll();

            var item = (from rol in rolQuery
                         join usuario in usuarioQuery
                         on rol.CreatorUserId equals usuario.Id
                         join usuarioAct in usuarioQuery
                         on rol.LastModifierUserId equals usuarioAct.Id into usuarioTemp
                         from usuarioActLeft in usuarioTemp.DefaultIfEmpty()
                         where rol.Id == rolId
                         select new RolDetalleDto
                         {
                             Id = rol.Id,
                             Nombre = rol.Nombre,
                             Codigo = rol.Codigo,
                             Url = rol.Url,
                             LastModificationTime = rol.LastModificationTime,
                             CreationTime = rol.CreationTime,
                             EsAdministrador = rol.EsAdministrador,
                             EsExterno = rol.EsExterno,
                             CreatorUser = usuario.Cuenta,
                             LastModifierUser = (usuarioActLeft != null) ? usuarioActLeft.Cuenta : string.Empty
                         }).FirstOrDefault();

            return item;
        }

        public override async Task<RolDto> InsertOrUpdateAsync(RolDto input)
        {
            var baseRepository = Repository as IBaseRepository<Rol>;
            if (baseRepository == null)
            {
                throw new ArgumentException(string.Format("The repository should be type IBaseRepository<TEntity>. {0}", Repository.GetType()), "input");
            }

            var entity = MapToEntity(input);

            var rolActualizado = await baseRepository.InsertOrUpdateAsync(entity);

            //En el caso que se cambien alguna propiedad  del rol : "Es Administrador" que afecte a la seguridad. Y se trate del mismo rol actualmente 
            //autentificado, volver a establecer 
            //if (_application.GetCurrentRol().Equals(entity))
            //{
            //    _application.SetCurrentRol(rolActualizado);
            //}


            //Reset cache
            ResetCache(rolActualizado.Codigo);

            return MapToEntityDto(rolActualizado);
        }

        


        /// <summary>
        /// Actualiza los permisos de un rol. Crear Permisos, o eliminar permisos, segun el listado de identificadores de acciones enviados
        /// </summary>
        /// <param name="rol">Rol</param>
        /// <param name="listAccionId">Lista de acciones de funcionalidades que debe posee</param>
        /// <returns></returns>
        public RolDto UpdatePermissions(int rolId, int[] listAccionId)
        {

            var baseRepository = Repository as IBaseRepository<Rol>;
            if (baseRepository == null)
            {
                throw new ArgumentException(string.Format("The repository should be type IBaseRepository<TEntity>. {0}", Repository.GetType()), "input");
            }

            var rol = baseRepository.Get(rolId, include => include.Permisos);


            Rol rolUpdate = null;

            if (listAccionId != null) //&& idRol != 0)
            {
                //TODO: JSA. TRANSACCION.

                //Delete
                List<int> permisosIdRol = (from i in rol.Permisos
                                           select i.AccionId).ToList();

                var eliminar = permisosIdRol.Except(listAccionId);

                //TODO: JSA, mejorar ahora se esta eliminando uno por uno las entidades.
                //Delete
                List<Permiso> permisosEliminar = new List<Permiso>();
                foreach (var item in eliminar)
                {
                    var permisoEliminar = rol.Permisos.Where(p => p.AccionId == item).FirstOrDefault();
                    //rol.Permisos.Remove(permisoEliminar);
                    //_repositoryPermiso.Delete(permisoEliminar);
                    permisosEliminar.Add(permisoEliminar);
                }


                if (permisosEliminar.Count() > 0)
                {

                    _repositoryPermiso.Delete(permisosEliminar);
                }


                foreach (var accion in listAccionId)
                {
                    var permiso = new Permiso();
                    permiso.RolId = rol.Id;
                    permiso.AccionId = accion;


                    //Add
                    if (!rol.Permisos.Where(c => c.AccionId == permiso.AccionId).Any())
                    {
                        rol.Permisos.Add(permiso);
                    }

                }

                rolUpdate = Repository.InsertOrUpdate(rol);

            }
            else
            {
                //TODO: JSA, mejorar ahora se esta eliminando uno por uno las entidades.
                //Eliminar todos los permisos del Rol

                _repositoryPermiso.Delete(rol.Permisos.ToList());

                rolUpdate = Repository.Get(rol.Id);
            }


            //Reset cache
            ResetCache(rol.Codigo);

            return MapToEntityDto(rolUpdate);

        }

       

 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async  Task<Tuple<bool, string>> CanRemoved(RolDto input)
        {
            var entity = MapToEntity(input);

            var resultado = entity.CanRemoved();
            if (!resultado.Item1)
                return resultado;

            var existentes = await Repository.LongCountAsync(r => r.Id != entity.Id);

            //No se puede eliminar todos los roles. Verificar si unicamente existe este rol.
            if (existentes == 0)
            {
                return new Tuple<bool, string>(false, string.Format("El rol [{0}], es el unico rol existente", entity.Nombre));
            }

            return new Tuple<bool, string>(true, "ok");
        }


        public async Task<RolPermisosDto> GetRolAndPermissions(int id)
        {
            var rol = (await GetAllRolesAndPermissionsInternal()).Where(r => r.Id == id).
                SingleOrDefault();
            
            return ObjectMapper.Map<RolPermisosDto>(rol);
        }

        public async Task<IEnumerable<RolPermisosDto>> GetAllRolAndPermissions()
        {

            var roles = (await GetAllRolesAndPermissionsInternal()).ToList();
            return roles;

        }

        /// <summary>
        /// Obtener todos los roles con sus permisos, desde cache si existe o desde la base de datos
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<RolPermisosDto>> GetAllRolesAndPermissionsInternal()
        {
            //Cache
            var rolesDto = _cacheManager.GetData(ConstantesCache.CACHE_ROLES_DTO_SISTEMA) as IEnumerable<RolPermisosDto>;
            if (rolesDto == null)
            {

                var roles = await Repository.GetAllIncluding(include => include.Permisos).ToListAsync();

                rolesDto = roles.Select(r => ObjectMapper.Map<RolPermisosDto>(r)).ToList();

                _cacheManager.Add(ConstantesCache.CACHE_ROLES_SISTEMA, rolesDto);
            }

            return rolesDto;
        }


        private void ResetCache(string codigoRol)
        {

            //Reset all Cache
            _cacheManager.Flush();

            //Reset cache
            //_cacheManager.Remove(ConstantesCache.CACHE_ROLES_SISTEMA);
            ////TODO: JSA. MEJORAR RESET Cache del Menu. ??, YA QUE TIENE UNA CLAVE POR MENU Y ROL
            //var codigoCacheMenu = "Udla.CarpetaLinea.Web.Cache.Menu." + ConstantesMenus.MENU_PRINCIPAL + "." + codigoRol;
            //_cacheManager.Remove(codigoCacheMenu);

            //TODO: JSA, que pasa si existen varios menus (Sitio docente, sitio del estudiante)

        }


    }
}
