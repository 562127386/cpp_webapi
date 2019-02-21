using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace com.cpp.calypso.seguridad.aplicacion
{
    public class ModuloService : AsyncBaseCrudAppService<Modulo, ModuloDto, PagedAndFilteredResultRequestDto>,
       IModuloService
    {
        private readonly ICacheManager CacheManager;

        public ModuloService(
             ICacheManager cacheManager,
             IBaseRepository<Modulo> repository) : base(repository)
        {
            this.CacheManager = cacheManager;
        }

        public ModuloDto Get(string codigo)
        {
            var rol = Repository.GetAll().Where(r => r.Codigo == codigo).SingleOrDefault();
            return MapToEntityDto(rol);
        }


        public override async Task<IList<ModuloDto>> GetAll()
        {
            var items = (await GetAllModuleAndFuncionalityInternal()).Select(r => ObjectMapper.Map<ModuloDto>(r))
               .ToList();

            return items; 
        }

        public async Task<ModuloFuncionalidDto> GetModuleAndFunctionality(int id)
        {
            var all = await GetAllModuleAndFuncionalityInternal();
            var item = all.Where(mod => mod.Id == id).SingleOrDefault();

            return ObjectMapper.Map<ModuloFuncionalidDto>(item); 
        }


        private async Task<IEnumerable<ModuloFuncionalidDto>> GetAllModuleAndFuncionalityInternal()
        {
           
                //Cache
                var datos = CacheManager.GetData(ConstantesCache.CACHE_MODULOS_SISTEMA) as IEnumerable<ModuloFuncionalidDto>;
                if (datos == null)
                {

                    var allModules = await Repository.GetAllIncluding(include => include.Funcionalidades).ToListAsync();
                    
                    datos = allModules.Select(mod => ObjectMapper.Map<ModuloFuncionalidDto>(mod)).ToList();

                    CacheManager.Add(ConstantesCache.CACHE_MODULOS_SISTEMA, datos);
                }

                return datos;
             

                
        }
    }
}
