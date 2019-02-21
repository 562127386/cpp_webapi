using Abp.AutoMapper;
using Abp.Modules;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.entityframework;
using com.cpp.calypso.framework;
using System.Reflection;

namespace com.cpp.calypso.proyecto.aplicacion
{
    [DependsOn(
         typeof(FrameworkModule),
         typeof(ComunEntityFrameworkModule),
         typeof(AbpAutoMapperModule),
          typeof(ComunAplicacionModule)
      )]
    public class ProyectoAplicacionModule : AbpModule
    {
        public override void PreInitialize()
        {



            //IocManager.Register<ConsultaFooPagedAndFilteredResultRequestDto, ConsultaFooPagedAndFilteredResultRequestDto>();
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
