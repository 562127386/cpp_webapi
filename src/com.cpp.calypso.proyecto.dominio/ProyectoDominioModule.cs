using Abp.Modules;
using com.cpp.calypso.comun.dominio;
using System.Reflection;

namespace com.cpp.calypso.seguridad.dominio
{
    [DependsOn(typeof(ComunDominioModule))]
    public class ProyectoDominioModule : AbpModule
    {
        public override void PreInitialize()
        {
  
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
