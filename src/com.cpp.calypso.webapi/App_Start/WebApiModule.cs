using Abp.Application.Services;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Runtime.Session;
using Abp.Web.Mvc;
using Abp.Web.Mvc.Configuration;
using Abp.WebApi;
using Castle.MicroKernel.Registration;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.comun.entityframework;
using com.cpp.calypso.framework;
using com.cpp.calypso.proyecto.aplicacion;
using com.cpp.calypso.proyecto.entityframework;
using com.cpp.calypso.seguridad.aplicacion;
using com.cpp.calypso.seguridad.entityframework;
using com.cpp.calypso.web;
using CommonServiceLocator;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace com.cpp.calypso.webapi.App_Start
{



    [DependsOn(
     typeof(ComunEntityFrameworkModule),
     typeof(SeguridadEntityFrameworkModule),
     typeof(ProyectoEntityFrameworkModule),

     typeof(ComunAplicacionModule),
     typeof(SeguridadAplicacionModule),
     typeof(ProyectoAplicacionModule),
     typeof(AbpWebMvcModule),
     typeof(AbpWebApiModule))]
    public class WebApiModule: AbpModule
    {

        public override void PreInitialize()
        {
        
            //LOG
            //Logs con Nlog, revisar la section nlog para las configuraciones
            IocManager.Register<ILoggerFactory, NLogFactory>(Abp.Dependency.DependencyLifeStyle.Singleton);
 
         
            //Gestion de Identidades 
            IocManager.Register<IIdentityUser, IdentityUser>(Abp.Dependency.DependencyLifeStyle.Singleton);
 
            //Gestion de Informacion de Usuarios Externos para sincronizacion de usuarios internos
            IocManager.Register<IExternalInfoUserProvider, ByPassInfoUserProvider>(Abp.Dependency.DependencyLifeStyle.Singleton);

 
            //VIEW
             IocManager.Register<ISerializerLayout, SerializerLayout>(Abp.Dependency.DependencyLifeStyle.Singleton);


            //TODO: Cambiar a PerWebRequest. Pendiente configuracion de Castle para soportar
            //IocManager.IocContainer.Register(
            //     Component
            //     .For<IViewService>()
            //     .ImplementedBy<ViewService>()
            //     .LifestyleTransient()
            //     //.LifestylePerWebRequest() 
            // );

            //Aplicacion personalizada para esta aplicacion.  
            IocManager.IocContainer.Register(
                Component
                .For<IApplication>()
                .ImplementedBy<GenericApiApplication>()
                .LifestyleTransient()
                //.LifestylePerWebRequest() 
            );


            ////Implementacion Instancia Default
            IocManager.IocContainer.Register(
                Component.For<PrincipalBaseDbContext>().
                ImplementedBy<PrincipalDbContext>()
                .Named("PrincipalDbContext")
                .LifestyleTransient()
                .IsDefault()
             );


        }



        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

    
            //Establecer el Service Locator con Windsor
            var windsorMapDependencyScope = new WindsorServiceLocator(IocManager.IocContainer);
            ServiceLocator.SetLocatorProvider(() => windsorMapDependencyScope);

  
            //Configuracion del webAPI
            WebApiConfig.Register(Configuration.Modules.AbpWebApi().HttpConfiguration);

            
            //Generar rest dinamicos desde los servicios de aplicacion
            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                            .ForAll<IApplicationService>(typeof(SeguridadAplicacionModule).Assembly, "app")
                            .WithConventionalVerbs()
                            .ForMethods(builder =>
                            {
                                if (builder.Method.Name.ToUpper().StartsWith("GET"))
                                {
                                    builder.Verb = Abp.Web.HttpVerb.Get;
                                }
                            })
                            .Build();


            //APlicar REST dinamico para ComunAplicacionModule, u otro modulo de servicios de Aplicacion

        }

        public override void PostInitialize()
        {
            //Desactivar validaciones de Abp en los controlladores
            Configuration.Modules.AbpMvc().IsValidationEnabledForControllers = false;

            //Desactivar antiforgery de abp
            Configuration.Modules.AbpWeb().AntiForgery.IsEnabled = false;

            //Desactivar validaciones de Abp en los controlladores API
            //Configuration.Modules.AbpWebApi().IsValidationEnabledForControllers = false;

            //abp. Ya posee binder para fechas
            //Aplicar Binder personalizado de fechas a todo el proyecto
            //var loggerFactory = IocManager.Resolve<ILoggerFactory>();
            //ModelBinders.Binders.Add(typeof(DateTime), new CustomDateBinder(loggerFactory));
            //ModelBinders.Binders.Add(typeof(DateTime?), new NullableCustomDateBinder());
            var session = IocManager.Resolve<IAbpSession>();
            var settingManager = IocManager.Resolve<ISettingManager>();
            //var settingStore = IocManager.Resolve<ISettingStore>();

            //var settingValues = await settingStore.GetAllListAsync(null, null);
             

            RegisterAdaptarDataAnnotationsCustom();
        }

        /// <summary>
        /// Registrar adaptadores para atributos DataAnnotations personalizados
        /// </summary>
        private void RegisterAdaptarDataAnnotationsCustom() {

            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(ObligadoAttribute),
                typeof(RequiredAttributeAdapter));


            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(LongitudMayorAttribute),
                typeof(MaxLengthAttributeAdapter));


            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RangoAttribute),
                typeof(RangeAttributeAdapter));  


        }
    }


}