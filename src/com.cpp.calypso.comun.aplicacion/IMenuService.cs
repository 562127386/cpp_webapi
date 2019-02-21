using Abp.Application.Services;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using CommonServiceLocator;
using System.Collections.Generic;
using System.Linq;

namespace com.cpp.calypso.comun.aplicacion
{
    public interface IMenuService:  IApplicationService
    {
        ICollection<MenuItem> GetItemMenuAutorizados(string codigoMenu, string codigoRol);

        ICollection<MenuItem> GetItemMenuAutorizados();
    }



    public class MenuService : IMenuService
    {
        private readonly ICacheManager cacheManager;
        private readonly IBaseRepository<Funcionalidad> repositoryFuncionalidad;
        private readonly IRolRepository<Rol> rolRepository;
        private readonly IBaseRepository<Accion> repositoryAccion;
        private readonly IBaseRepository<MenuItem> repositoryMenuItem;
        private readonly IRepositoryNamed<Menu> repositoryMenu;

        public MenuService(
            ICacheManager cacheManager,
            IBaseRepository<Funcionalidad> repositoryFuncionalidad,
            IRolRepository<Rol> rolRepository,
            IBaseRepository<Accion> repositoryAccion,
            IBaseRepository<MenuItem> repositoryMenuItem,
            IRepositoryNamed<Menu> repositoryMenu)
        {
            this.cacheManager = cacheManager;
            this.repositoryFuncionalidad = repositoryFuncionalidad;
            this.rolRepository = rolRepository;
            this.repositoryAccion = repositoryAccion;
            this.repositoryMenuItem = repositoryMenuItem;
            this.repositoryMenu = repositoryMenu;
        }

        /// <summary>
        /// Obtener items de Menus autentificados
        /// </summary>
        /// <param name="codigoModulo"></param>
        /// <returns></returns>
        public ICollection<MenuItem> GetItemMenuAutorizados()
        {
            var app = ServiceLocator.Current.GetInstance<IApplication>();

            if (!app.IsAuthenticated())
                return null;

            var cache = ServiceLocator.Current.GetInstance<ICacheManager>();

            //Obtener los roles del usuario
            var usuario = app.GetCurrentUser();
            if (usuario == null)
                return null;

            var codigoCache = "Items.Menu." + app.GetCurrentModule().Codigo + "." + usuario.Cuenta;

            var menuCache = cache.GetData(codigoCache) as ICollection<MenuItem>;

            if (menuCache == null)
            {
                var moduloId = app.GetCurrentModule().Id;
                //1. Obtener Menu asociado al Modulo.
                var menuModulo = (from m in repositoryMenu.GetAll()
                                  where m.ModuloId == moduloId
                                  select m).FirstOrDefault();

                if (menuModulo == null) {
                    string error = string.Format("No existe menus, asociados al modulo [{0}]", app.GetCurrentModule().Codigo);
                    throw new GenericException(error, error);
                }

                var menuCodigo = menuModulo.Codigo;

                var tieneRolAdministrador = usuario.Roles.Where(r => r.EsAdministrador).Any();

                if (tieneRolAdministrador)
                {
                    var menu = repositoryMenu.Get(menuCodigo, include => include.Items);
                    if (menu != null)
                        menuCache = menu.Items;
                }
                else {
                    //Obtener todos los items de menus..
                    var rolIds = usuario.Roles.Select(r => r.Id).ToArray();

                    menuCache = (from r in rolRepository.GetAll().Where(r => rolIds.Contains(r.Id))
                                 from p in r.Permisos
                                 join a in repositoryAccion.GetAll()
                                 on p.AccionId equals a.Id
                                 join f in repositoryFuncionalidad.GetAll()
                                 on a.FuncionalidadId equals f.Id
                                 join m in repositoryMenuItem.GetAll()
                                 on f.Id equals m.FuncionalidadId
                                 join mnu in repositoryMenu.GetAll()
                                 on m.MenuId equals mnu.Id
                                 where mnu.Codigo == menuCodigo
                                 select m
                            ).Distinct().
                            //Menus sin funcionalidades asociadas
                            Union(
                                from m in repositoryMenuItem.GetAll()
                                join mnu in repositoryMenu.GetAll()
                                on m.MenuId equals mnu.Id
                                where m.FuncionalidadId == null
                                && mnu.Codigo == menuCodigo
                                select m
                            ).ToList();
                }

                

                if (menuCache == null)
                {
                    string error = string.Format("No existe items de menus autorizados [{0}]", menuModulo.Codigo);
                    throw new GenericException(error, error);
                }

                cache.Add(codigoCache, menuCache);

            }

            return menuCache;
        }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="codigoMenu"></param>
            /// <param name="codigoRol"></param>
            /// <returns></returns>
            public ICollection<MenuItem> GetItemMenuAutorizados(string codigoMenu, string codigoRol) {
            //TODO: Mejorar generacion de Menu
            //1. Aplicar Seguridad
            //2. Estilos
            //3. Cache o guardar Session
            //4. Menus Jerarquicos


            var app = ServiceLocator.Current.GetInstance<IApplication>();

            if (!app.IsAuthenticated())
                return null;

            var cache = ServiceLocator.Current.GetInstance<ICacheManager>();

        
            //TODO: JSA, como realizar reset cuando se cambia la seguridad en roles ??? 
            var codigoCache = "Items.Menu." + codigoMenu.Trim() + "." + codigoRol;

            var menuCache = cache.GetData(codigoCache) as ICollection<MenuItem>;

            if (menuCache == null)
            {
                
                //if (app.GetCurrentRol().EsAdministrador)
                //{
                //    //El Rol Administrador tiene todos los items de menu
                //    var menu = repositoryMenu.Get(codigoMenu, include => include.Items);
                //    if (menu != null)
                //        menuCache = menu.Items;
                //}
                //else
                //{
                    menuCache = (from r in rolRepository.GetAll().Where(r => r.Codigo == codigoRol)
                                 from p in r.Permisos
                                 join a in repositoryAccion.GetAll()
                                 on p.AccionId equals a.Id
                                 join f in repositoryFuncionalidad.GetAll()
                                 on a.FuncionalidadId equals f.Id
                                 join m in repositoryMenuItem.GetAll()
                                 on f.Id equals m.FuncionalidadId
                                 join mnu in repositoryMenu.GetAll()
                                 on m.MenuId equals mnu.Id
                                 where mnu.Codigo == codigoMenu
                                 select m
                            ).Distinct().
                            //Menus sin funcionalidades asociadas
                            Union(
                                from m in repositoryMenuItem.GetAll()
                                join mnu in repositoryMenu.GetAll()
                                on m.MenuId equals mnu.Id
                                where m.FuncionalidadId == null
                                && mnu.Codigo == codigoMenu
                                select m
                            ).ToList();

                //}

                if (menuCache == null)
                {
                    string error = string.Format("No existe le menún [{0}]", codigoMenu);
                    throw new GenericException(error, error);
                }

                cache.Add(codigoCache, menuCache);
           
            }

            return menuCache; 
        }
    }
}
