using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.MicroKernel.Registration;
using com.cpp.calypso.comun.aplicacion;
using com.cpp.calypso.comun.dominio;
using com.cpp.calypso.comun.entityframework;
using com.cpp.calypso.framework;
using System.Configuration;
using System.Reflection;
using Z.EntityFramework.Plus;

namespace com.cpp.calypso.seguridad.aplicacion
{
    [DependsOn(
       typeof(FrameworkModule),
       typeof(ComunEntityFrameworkModule),
       typeof(AbpAutoMapperModule),
        typeof(ComunAplicacionModule)
    )]
    public class SeguridadAplicacionModule : AbpModule
    {
        public override void PreInitialize()
        {

        
            //Autentificacion
            IocManager.Register<IAuthentication, TableAuthentication>();

            //Autorizacion
            IocManager.Register<IAuthorizationService, AuthorizationService>();

            IocManager.Register<IRolService, RolService>();

            IocManager.Register<IUsuarioService, UsuarioService>();

            IocManager.Register<ISesionService, SesionService>();

            var optionGetUserExternal = ConfigurationManager.AppSettings["Security.option.userExternal"];
            if (optionGetUserExternal == "LDAP")
            {

                var ldapDomain = ConfigurationManager.AppSettings["Security.ldap.domain"];
                var ldapConection = ConfigurationManager.AppSettings["Security.ldap.conection"];
                var ldapUser = ConfigurationManager.AppSettings["Security.ldap.user"];
                var ldapPassword = ConfigurationManager.AppSettings["Security.ldap.password"];
                var ldapAttributeMapUsername = ConfigurationManager.AppSettings["Security.ldap.AttributeMapUsername"];
                var ldapFilterSearchUser = ConfigurationManager.AppSettings["Security.ldap.filterSearchUser"];
                var ldapAuthenticationType = ConfigurationManager.AppSettings["Security.ldap.authenticationType"];
                var ldapAuthenticationTypeInt = 0;
                int.TryParse(ldapAuthenticationType, out ldapAuthenticationTypeInt);

                IocManager.IocContainer.Register(Component.For(typeof(IUserExternalSouce))
                   .ImplementedBy(typeof(LdapUserExternalSouce))
                   .UsingFactoryMethod(() => new LdapUserExternalSouce(ldapDomain,
                        ldapConection,
                        ldapUser, ldapPassword, ldapAttributeMapUsername,
                        ldapFilterSearchUser, ldapAuthenticationTypeInt))
                   .LifeStyle.Singleton);

            }
            else
            {

                //AD
                //Default

                var activeDirectoryDomain = ConfigurationManager.AppSettings["Security.ad.domain"];
                var activeDirectoryConection = ConfigurationManager.AppSettings["Security.ad.conection"];
                var activeDirectoryConectionUserName = ConfigurationManager.AppSettings["Security.ad.user"];
                var activeDirectoryConectionPassword = ConfigurationManager.AppSettings["Security.ad.password"];



                IocManager.IocContainer.Register(Component.For(typeof(IUserExternalSouce))
                   .ImplementedBy(typeof(ActiveDirectoryUserExternalSouce))
                   .UsingFactoryMethod(() => new ActiveDirectoryUserExternalSouce(activeDirectoryDomain,
                        activeDirectoryConection,
                        activeDirectoryConectionUserName, activeDirectoryConectionPassword))
                   .LifeStyle.Singleton);
            }

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());


            // Configurar los mappers entre objetos
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                cfg.CreateMap<ModuloDto, int>().ConvertUsing(r => r.Id);
                cfg.CreateMap<RolDto, string>().ConvertUsing(r => r.Nombre);


          

                cfg.CreateMap<CrearUsuarioDto, Usuario>()
                        .ForMember(x => x.Roles, opt => opt.Ignore())
                        .ForMember(x => x.Modulos, opt => opt.Ignore());


                cfg.CreateMap<AuditoriaEntidad, AuditoriaDto>()
                    ///.ForMember(x => x.Id, opt => opt.Ignore())
                    ;


                cfg.CreateMap<AuditEntryProperty, AuditoriaPropiedadDto>()
                      //.ForMember(x => x.Id, opt => opt.Ignore())
                      ;

            });

        }

        public override void PostInitialize()
        {

        }
    }
}
