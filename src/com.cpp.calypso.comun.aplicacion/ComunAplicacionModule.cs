using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.MicroKernel.Registration;
using com.cpp.calypso.comun.entityframework;
using com.cpp.calypso.framework;
using System.Reflection;
using Abp.Configuration.Startup;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.comun.aplicacion
{
    [DependsOn(
      typeof(FrameworkModule),
      typeof(ComunEntityFrameworkModule),
      typeof(AbpAutoMapperModule)
   )]
    public class ComunAplicacionModule : AbpModule
    {
        public override void PreInitialize()
        {


            //Implementacion de Servicio Parametros y Gestor de Parametros. Instancia Default
            IocManager.IocContainer.Register(
                Component.For<IParametroService, IParametroManager>().
                ImplementedBy<ParametroService>()
                .Named("ParametroService")
                .LifestyleTransient()
                .IsDefault()

             );

            IocManager.Register<IPagedAndFilteredResultRequestDto, PagedAndFilteredResultRequestDto>(
DependencyLifeStyle.Transient);


            //TMP
            IocManager.IocContainer.Register(Component.For(typeof(IEntityService<>))
             .ImplementedBy(typeof(GenericService<>))
             .LifeStyle.Transient);

            //Inyectar servicios de aplicaciones base de CRUD.
            IocManager.IocContainer.Register(Component.For(typeof(IAsyncBaseCrudAppService<,,>))
       .ImplementedBy(typeof(AsyncBaseCrudAppService<,,>))
       .LifeStyle.Transient);

            IocManager.IocContainer.Register(Component.For(typeof(IAsyncBaseCrudAppService<,,,>))
      .ImplementedBy(typeof(AsyncBaseCrudAppService<,,,>))
      .LifeStyle.Transient);

         
            //Reemplazar implementaciones
            Configuration.ReplaceService<ISettingStore, AppSettingAbpSettingStore>(DependencyLifeStyle.Transient);

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
