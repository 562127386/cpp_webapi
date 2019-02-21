using Abp.EntityFramework;
using Abp.Modules;
using Castle.MicroKernel.Registration;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.comun.entityframework;
using com.cpp.calypso.framework;
 
using System.Reflection;

namespace com.cpp.calypso.seguridad.entityframework
{
    [DependsOn(
       typeof(ComunEntityFrameworkModule),
       typeof(FrameworkModule),
       typeof(AbpEntityFrameworkModule)
     )]
    public class SeguridadEntityFrameworkModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = Constantes.ConnectionStringName;
   

            

    
            //TODO: Cambiar  IUsuarioRepository<>: IXX<> => IUsuarioRepository : IXX <Uusuario>
            //
            //Repositorios Personalizados 
            IocManager.IocContainer.Register(Component.For(typeof(IRolRepository<>))
            .ImplementedBy(typeof(EntityFrameworkRolRepository<>))
            .LifeStyle.Transient);

            IocManager.IocContainer.Register(Component.For(typeof(IUsuarioRepository<>))
            .ImplementedBy(typeof(EntityFrameworkUsuarioRepository<>))
            .LifeStyle.Transient);

            IocManager.IocContainer.Register(Component.For(typeof(ISesionRepository<>))
              .ImplementedBy(typeof(EntityFrameworkSesionRepository<>))
              .LifeStyle.Transient);

 
            IocManager.IocContainer.Register(Component.For(typeof(IAuditoriaRepository<>))
            .ImplementedBy(typeof(EntityFrameworkAuditoriaRepository<>))
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
