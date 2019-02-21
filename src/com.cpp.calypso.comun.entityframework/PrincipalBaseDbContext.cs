using Abp.EntityFramework;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using System.Data.Common;
using System.Data.Entity;

namespace com.cpp.calypso.comun.entityframework
{
    public abstract class PrincipalBaseDbContext :    AbpDbContext
    {

       
        public DbSet<Modulo> Modulos { get; set; }

        public DbSet<Funcionalidad> Funcionalidades { get; set; }
 

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Rol> Roles { get; set; }


        public DbSet<Permiso> Permisos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Sesion> Sesiones { get; set; }

       
        public DbSet<ParametroSistema> Parametros { get; set; }

        //Vistas Dinamicas
        public DbSet<View> Views { get; set; }


       


        /// <summary>
        /// used by the EF command-line migration tool commands (like "update-database").
        /// </summary>
        public PrincipalBaseDbContext()
       : base(Constantes.ConnectionStringName)
        {


        }

        /// <summary>
        /// The constructor gets the nameOrConnectionString which is used by ABP to pass the connection name or string on runtime.
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        public PrincipalBaseDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {


//#if DEBUG
//            var factory = new NLogFactory();
//            log = factory.Create(typeof(PrincipalBaseDbContext));
//            this.Database.Log = l => log.Debug(l);
//#endif
        }

        /// <summary>
        /// The constructor get the existingConnection which can be used for unit tests, and is not directly
        /// </summary>
        /// <param name="existingConnection"></param>
        public PrincipalBaseDbContext(DbConnection existingConnection)
            : base(existingConnection, false)
        {
//#if DEBUG
//            var factory = new NLogFactory();
//            log = factory.Create(typeof(PrincipalBaseDbContext));
//            this.Database.Log = l => log.Debug(l);
//#endif
        }

        /// <summary>
        /// The constructor gets the existingConnection and the contextOwnsConnection is used by ABP on single database/ multiple dbcontext scenarios to share the same connection & transaction () when DbContextEfTransactionStrategy is used
        /// </summary>
        /// <param name="existingConnection"></param>
        /// <param name="contextOwnsConnection"></param>
        public PrincipalBaseDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
//#if DEBUG
//            var factory = new NLogFactory();
//            log = factory.Create(typeof(PrincipalBaseDbContext));
//            this.Database.Log = l => log.Debug(l);
//#endif

        }


    }
}
