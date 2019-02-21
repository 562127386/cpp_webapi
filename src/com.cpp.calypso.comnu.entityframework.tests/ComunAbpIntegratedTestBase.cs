using Abp.EntityFramework;
using Abp.Modules;
using Abp.TestBase;
using Castle.MicroKernel.Registration;
using com.cpp.calypso.comun.entityframework;
using EntityFramework.DynamicFilters;
using System;
using System.Data.Common;
using System.Data.Entity;

namespace com.cpp.calypso.comun.entityframework.tests
{
    /// <summary>
    /// Clase base integrado con ABP, para realizar pruebas del modulo Comun / Entity Framework
    /// </summary>
    public abstract class ComunEntityFrameworkAbpIntegratedTestBase : AbpIntegratedTestBase<ComunEntityFrameworkAbpIntegratedTestModule>
    {
        protected ComunEntityFrameworkAbpIntegratedTestBase()
        {
            //Seed initial data
            UsingDbContext(context => new ComunEntityFrameworkAbpIntegratedInitialDataBuilder().Build(context));
        }

        protected  override void PreInitialize()
        {
            //Fake DbConnection using Effort!
            LocalIocManager.IocContainer.Register(
                Component.For<ComunEntityFrameworkAbpIntegratedDbContext>()
                     //Usar dos diferentes escenarios
                     //https://tflamichblog.wordpress.com/2012/11/04/factory-methods-in-effort-createtransient-vs-createpersistent/
                     //.UsingFactoryMethod(() => new ComunAbpIntegratedDbContext(Effort.DbConnectionFactory.CreateTransient()))
                     .UsingFactoryMethod(() => new ComunEntityFrameworkAbpIntegratedDbContext(Effort.DbConnectionFactory.CreatePersistent("db.ComunEntityFrameworkAbpIntegratedTestBase.test")))
                     //.ImplementedBy<ComunbpIntegratedDbContext>()
                    .LifestyleTransient()
                );

            base.PreInitialize();
        }

        public void UsingDbContext(Action<ComunEntityFrameworkAbpIntegratedDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<ComunEntityFrameworkAbpIntegratedDbContext>())
            {
                context.DisableAllFilters();
                action(context);
                context.SaveChanges();
            }
        }

        public T UsingDbContext<T>(Func<ComunEntityFrameworkAbpIntegratedDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<ComunEntityFrameworkAbpIntegratedDbContext>())
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
    public class ComunEntityFrameworkAbpIntegratedInitialDataBuilder
    {
        public void Build(ComunEntityFrameworkAbpIntegratedDbContext context)
        {
            
        }
    }

    /// <summary>
    /// Contexto de base de datos, para pruebas
    /// </summary>
    [DefaultDbContext]
    public class ComunEntityFrameworkAbpIntegratedDbContext : PrincipalBaseDbContext
    {


        /// <summary>
        /// Agregar nuevas Entidades, para pruebass
        /// </summary>

       
        //public virtual IDbSet<Task> Tasks { get; set; }
        //public virtual IDbSet<Person> People { get; set; }

        public ComunEntityFrameworkAbpIntegratedDbContext()
            : base("ComunEntityFrameworkDbContextTest")
        {

        }

        public ComunEntityFrameworkAbpIntegratedDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public ComunEntityFrameworkAbpIntegratedDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }


    /// <summary>
    /// Modulo 
    /// </summary>
    [DependsOn(
         typeof(ComunEntityFrameworkModule),
         typeof(AbpTestBaseModule)
     )]
    public class ComunEntityFrameworkAbpIntegratedTestModule : AbpModule
    {

    }
}
