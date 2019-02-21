using Abp.EntityFramework;
using Abp.Modules;
using Abp.TestBase;
using Castle.MicroKernel.Registration;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.comun.entityframework;
using EntityFramework.DynamicFilters;
using System;
using System.Data.Common;
using System.Data.Entity;

namespace com.cpp.calypso.tests
{
    /// <summary>
    /// Clase base integrado con ABP, para realizar pruebas del modulo Comun.
    /// </summary>
    public abstract class ComunAbpIntegratedTestBase : AbpIntegratedTestBase<ComunAbpIntegratedTestModule>
    {
        protected ComunAbpIntegratedTestBase()
        {
            //Seed initial data
            UsingDbContext(context => new ComunAbpIntegratedInitialDataBuilder().Build(context));
        }

        protected  override void PreInitialize()
        {
            //Fake DbConnection using Effort!
            LocalIocManager.IocContainer.Register(
                Component.For<ComunAbpIntegratedDbContext>()
                     //Usar dos diferentes escenarios
                     //https://tflamichblog.wordpress.com/2012/11/04/factory-methods-in-effort-createtransient-vs-createpersistent/
                     //.UsingFactoryMethod(() => new ComunAbpIntegratedDbContext(Effort.DbConnectionFactory.CreateTransient()))
                     .UsingFactoryMethod(() => new ComunAbpIntegratedDbContext(Effort.DbConnectionFactory.CreatePersistent("db.ComunAbpIntegratedTestBase.test")))
                     //.ImplementedBy<ComunbpIntegratedDbContext>()
                    .LifestyleTransient()
                );

            base.PreInitialize();
        }

        public void UsingDbContext(Action<ComunAbpIntegratedDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<ComunAbpIntegratedDbContext>())
            {
                context.DisableAllFilters();
                action(context);
                context.SaveChanges();
            }
        }

        public T UsingDbContext<T>(Func<ComunAbpIntegratedDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<ComunAbpIntegratedDbContext>())
            {
                context.DisableAllFilters();
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }
    }

    /// <summary>
    /// Crear datos iniciales para pruebas
    /// </summary>
    public class ComunAbpIntegratedInitialDataBuilder
    {
        public void Build(ComunAbpIntegratedDbContext context)
        {
            //Agregar Modulos, Funcionalidad, Action    
            var data = EntidadesComunFake.FakeFuncionalidades(5);
            
            foreach (var item in data)
            {
                context.Funcionalidades.Add(item);
            }


            var usuarios = EntidadesComunFake.FakeAgregarUsuarios(data[0].Modulo, 4);

            foreach (var item in usuarios)
            {
                context.Usuarios.Add(item);
            }

            context.SaveChanges();
        }
    }

    /// <summary>
    /// Contexto de base de datos, para pruebas
    /// </summary>
    [DefaultDbContext]
    public class ComunAbpIntegratedDbContext : PrincipalBaseDbContext
    {


        /// <summary>
        /// Agregar nuevas Entidades, para pruebass
        /// </summary>

        public DbSet<Secuencia> Secuencias { get; set; }

        public DbSet<DestinoSecuencia> DestinoSecuencia { get; set; }

        //public virtual IDbSet<Task> Tasks { get; set; }
        //public virtual IDbSet<Person> People { get; set; }

        public ComunAbpIntegratedDbContext()
            : base("ComunDbContextTest")
        {

        }

        public ComunAbpIntegratedDbContext(string nameOrConnectionString)
            : base("ComunDbContextTest")
        {

        }

        //This constructor is used in tests
        public ComunAbpIntegratedDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }


    /// <summary>
    /// Modulo 
    /// </summary>
    [DependsOn(
         typeof(ComunAplicacionModule),
         typeof(ComunDominioModule),
         typeof(AbpTestBaseModule)
     )]
    public class ComunAbpIntegratedTestModule : AbpModule
    {

    }
}
