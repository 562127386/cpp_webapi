using Abp;
using Abp.Modules;
using Castle.MicroKernel.Registration;
using System.Configuration;
using System.Reflection;

namespace com.cpp.calypso.comun.dominio
{
    [DependsOn(typeof(AbpKernelModule))]
    public class ComunDominioModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Rol
            IocManager.IocContainer.Register(Component.For(typeof(AspRoleStore<Rol, Usuario>))
            .ImplementedBy(typeof(RoleStore))
            .LifeStyle.Transient);

            IocManager.IocContainer.Register(Component.For(typeof(AspRoleManager<Rol, Usuario>))
           .ImplementedBy(typeof(RoleManager))
           .LifeStyle.Transient);

            //Module
            IocManager.IocContainer.Register(Component.For(typeof(BaseModuleStore<Modulo, Usuario>))
          .ImplementedBy(typeof(ModuleStore))
          .LifeStyle.Transient);

            IocManager.IocContainer.Register(Component.For(typeof(BaseModuleManager<Modulo, Usuario>))
        .ImplementedBy(typeof(ModuleManager))
        .LifeStyle.Transient);

            //User
            IocManager.IocContainer.Register(Component.For(typeof(AspUserStore<Rol,Usuario>))
             .ImplementedBy(typeof(UserStore))
             .LifeStyle.Transient);

            IocManager.IocContainer.Register(Component.For(typeof(AspUserManager<Rol,Usuario,Modulo>))
            .ImplementedBy(typeof(UserManager))
            .LifeStyle.Transient);



            var byPassPasswordStr = ConfigurationManager.AppSettings["Security.byPassPassword"];
            var byPassPassword = false;
            bool.TryParse(byPassPasswordStr, out byPassPassword);

            if (!byPassPassword)
            {
                //Password Manager
                //Opcion 1. Base de datos
                /*
                IocManager.IocContainer.Register(Component.For(typeof(IPasswordManager<Usuario>))
                    .ImplementedBy(typeof(DbPasswordManager))
                    .LifeStyle.Transient);

                */

                //Opcion 2. Directorio Activo
                //TODO: 
                var activeDirectoryDomain = ConfigurationManager.AppSettings["Security.ad.domain"];
                var activeDirectoryConectionUserName = ConfigurationManager.AppSettings["Security.ad.user"];
                var activeDirectoryConectionPassword = ConfigurationManager.AppSettings["Security.ad.password"];


                IocManager.IocContainer.Register(Component.For(typeof(IPasswordManager<Usuario>))
                   .ImplementedBy(typeof(DbPasswordManager))
                   .UsingFactoryMethod(() => new ActiveDirectoryPasswordManager(activeDirectoryDomain,
                        activeDirectoryConectionUserName, activeDirectoryConectionPassword))
                   .LifeStyle.Transient);
            }
            else
            {

                IocManager.IocContainer.Register(Component.For(typeof(IPasswordManager<Usuario>))
                   .ImplementedBy(typeof(ByPassPasswordManager))
                   .LifeStyle.Transient);

            }

            IocManager.IocContainer.Register(Component.For(typeof(LoginManager<Usuario, Modulo,Rol>))
            .ImplementedBy(typeof(LoginManager<Usuario, Modulo,Rol>))
            .LifeStyle.Transient);
 
          }


         public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {

        }
    }
}
