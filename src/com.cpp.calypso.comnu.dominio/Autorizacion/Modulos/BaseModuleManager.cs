using System;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace com.cpp.calypso.comun.dominio
{
    /// <summary>
    /// Gestor de Modulos
    /// </summary>
    public class ModuleManager : BaseModuleManager<Modulo, Usuario>
    {
        public ModuleManager(BaseModuleStore<Modulo, Usuario> moduleStore) :
            base(moduleStore)
        {

        }
    }

    public abstract class BaseModuleManager<TModule, TUser> :
         IDomainService
        where TModule : Modulo
        where TUser : AspUser<TUser>
    {
        private readonly BaseModuleStore<TModule, TUser> ModuleStore;

        protected BaseModuleManager(BaseModuleStore<TModule, TUser> moduleStore )
        {
            this.ModuleStore = moduleStore;
        }

        public async Task<TModule> GetModuleByNameAsync(string nombre)
        {
            var modulo = await ModuleStore.FindByNameAsync(nombre);
            if (modulo == null)
            {
                throw new Exception("There is no module with name: " + nombre);
            }

            return modulo;
        }

        public async Task<TModule> GetModuleByIdAsync(int moduloId)
        {
            var modulo = await ModuleStore.FindByIdAsync(moduloId);
            if (modulo == null)
            {
                throw new Exception("There is no module with id: " + moduloId);
            }

            return modulo;
        }

        public async Task<TModule> FindByIdAsync(int moduloId)
        {
            var modulo = await ModuleStore.FindByIdAsync(moduloId);
            if (modulo == null)
            {
                throw new Exception("There is no module with id: " + moduloId);
            }

            return modulo;
        }
    }
}
