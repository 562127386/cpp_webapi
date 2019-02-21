using System.Threading.Tasks;
using Abp.Dependency;

namespace com.cpp.calypso.comun.dominio
{
    public class ModuleStore : BaseModuleStore<Modulo, Usuario>
    {
        public ModuleStore(IBaseRepository<Modulo> moduloRepository) : base(moduloRepository)
        {

        }
    }

    public abstract class BaseModuleStore<TModule, TUser> :
      ITransientDependency
      where TModule : Modulo
      where TUser : AspUser<TUser>
    {
        private readonly IBaseRepository<TModule> moduloRepository;

        protected BaseModuleStore(IBaseRepository<TModule> moduloRepository)
        {
            this.moduloRepository = moduloRepository;
        }

        public async Task<TModule> FindByNameAsync(string nombre)
        {
            return await moduloRepository.FirstOrDefaultAsync(
               modulo => modulo.Nombre  == nombre
               );
        }

        public async Task<TModule> FindByIdAsync(int moduloId)
        {
            return await moduloRepository.FirstOrDefaultAsync(moduloId);
        }
    }
}
