using System.Data.Entity;
using System.Linq;
using Abp.EntityFramework;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.comun.entityframework;

namespace com.cpp.calypso.seguridad.entityframework
{

    public class EntityFrameworkUsuarioRepository<TUsuario> : BaseRepository<TUsuario>, IUsuarioRepository<TUsuario> 
        where TUsuario: Usuario
    {

      

        public EntityFrameworkUsuarioRepository(IDbContextProvider<PrincipalBaseDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }


        //public EntityFrameworkUsuarioRepository(
        //    DbContext context,
        //    IIdentityUser identityUser,
        //    IManagerDateTime managerDateTime)
        //    : base(context, identityUser, managerDateTime)
        //{

        //}


        public TUsuario Get(string cuenta)
        {

            //var usuario =  GetSet().Where(c => c.Cuenta == cuenta).SingleOrDefault();

            //return usuario;

            var usuario = GetAll().AsNoTracking().Where(c => c.Cuenta == cuenta)
                .Include(u => u.Roles).SingleOrDefault();

            return usuario;
            ////return GetSet().AsNoTracking().Where(c => c.Cuenta == cuenta)
            ////    .Include(u => u.Roles).SingleOrDefault();

        }
    }
}
