using Abp.EntityFramework;
using Abp.Modules;
using com.cpp.calypso.comun.entityframework;
using com.cpp.calypso.framework;
using System.Reflection;

namespace com.cpp.calypso.proyecto.entityframework
{
    [DependsOn(
      typeof(ComunEntityFrameworkModule),
     typeof(FrameworkModule),
        typeof(AbpEntityFrameworkModule)
      )]
    public class ProyectoEntityFrameworkModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = Constantes.ConnectionStringName;
 
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
