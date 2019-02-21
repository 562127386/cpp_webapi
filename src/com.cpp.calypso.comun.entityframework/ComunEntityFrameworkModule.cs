using Abp.EntityFramework;
using Abp.Modules;
using Castle.MicroKernel.Registration;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.framework;
using System.Reflection;

namespace com.cpp.calypso.comun.entityframework
{
    [DependsOn(
     typeof(ComunDominioModule),
    typeof(FrameworkModule),
       typeof(AbpEntityFrameworkModule)
     )]
    public class ComunEntityFrameworkModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = Constantes.ConnectionStringName;
   

           //Repositorio Base
            IocManager.IocContainer.Register(Component.For(typeof(IBaseRepository<>))
           .ImplementedBy(typeof(BaseRepository<>))
           .LifeStyle.Transient);

            IocManager.IocContainer.Register(Component.For(typeof(IRepositoryNamed<>))
             .ImplementedBy(typeof(BaseRepositoryNamed<>))
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
