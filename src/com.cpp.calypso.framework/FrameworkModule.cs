using Abp.Modules;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using com.cpp.calypso.comun.dominio;
using System.Reflection;

namespace com.cpp.calypso.framework
{
    [DependsOn(
      typeof(ComunDominioModule)
      )]
    public class FrameworkModule : AbpModule
    {
        public override void PreInitialize()
        {

            //Cache local, en memoria
            IocManager.Register<ICacheManager, LocalCacheManager>(Abp.Dependency.DependencyLifeStyle.Singleton);

 
            //Collecion....
            IocManager.IocContainer.Kernel.Resolver.AddSubResolver(new CollectionResolver(IocManager.IocContainer.Kernel));
   
            IocManager.IocContainer.Register(

                Component.For<IFilterHandleException>().
                ImplementedBy<ElmahFilterHandleException>()
                .Named("ElmahFilterHandleException").
                LifestyleSingleton().
                IsDefault(),

                Component.For<IFilterHandleException>().
                ImplementedBy<ApplyFriendlyMessageIFilterHandleException>()
                .Named("ApplyFriendlyMessageIFilterHandleException")
                .LifestyleSingleton()
            
             );


            IocManager.Register<IHandlerExcepciones, ManejadorExcepciones>(
                Abp.Dependency.DependencyLifeStyle.Singleton);

           
            //Creacion de objetos
            IocManager.Register<ICreateObject, FastCreateObject>(Abp.Dependency.DependencyLifeStyle.Singleton);


            //Obtener fechas desde la maquina que se ejecuta la aplicacion
            IocManager.Register<IManagerDateTime, LocalManagerDateTime>(Abp.Dependency.DependencyLifeStyle.Singleton);



            //No se puede usar RazorLightTemplateEngine. existe un error
            //GetEntryAssembly returns null?
            //IocManager.IocContainer.Register(
            //Component.For<ITemplateEngine>()
            //     .UsingFactoryMethod(() => new RazorLightTemplateEngine("Templates"))
            //    .LifestyleSingleton()
            //);

            IocManager.IocContainer.Register(
             Component.For<ITemplateEngine>()
                  .UsingFactoryMethod(() => new RazorTemplateEngine(new FileRepositoryTemplates("Templates")))
                 .LifestyleSingleton()
            );

            



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
