using Abp.Domain.Entities;
using com.cpp.calypso.comun.entityframework;
using com.cpp.calypso.framework;
using com.cpp.calypso.seguridad.aplicacion;
using com.cpp.calypso.seguridad.entityframework;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using com.cpp.calypso.proyecto.dominio.Entidades;
using com.cpp.calypso.proyecto.entityframework.Mapping;
using Z.EntityFramework.Plus;

namespace com.cpp.calypso.web
{
    public  class PrincipalDbContext : PrincipalBaseDbContext
    {

        public ILoggerFactory iLoggerFactory { get; set; }
        private ILogger log;


        #region Entidades Auditoria

        public DbSet<AuditoriaEntidad> Auditorias { get; set; }
        public DbSet<AuditoriaPropiedad> AuditoriasPropiedades { get; set; }


        //public DbSet<AuditEntry> AuditEntries { get; set; }
        //public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; }
        public DbSet<TipoCatalogo> TipoCatalogo { get; set; }
        #endregion



        protected static ConcurrentDictionary<string, Dictionary<string, string>> AuditedDictionary
                = new ConcurrentDictionary<string, Dictionary<string, string>>();

        static PrincipalDbContext()
        {

            // Globally
            AuditManager.DefaultConfiguration.IgnorePropertyUnchanged = false;

            //1. Load Configuration audit
            var configuracionStr = ConfigurationManager.AppSettings[com.cpp.calypso.webapi.Constantes.SEGURIDAD_AUDITORIA_CONFIGURACION];
            var configuracion = JsonConvert.DeserializeObject<List<AuditoriaConfiguracion>>(configuracionStr);

            foreach (var item in configuracion)
            {
                var itemProperty = new Dictionary<string, string>();
                foreach (var propiedad in item.Propiedades)
                {
                    itemProperty.Add(propiedad.ToUpper(), propiedad);
                }
                AuditedDictionary.TryAdd(item.Entidad.ToUpper(), itemProperty);
            }

            //2. Custom Class: Globally
            AuditManager.DefaultConfiguration
                .AuditEntryFactory = args =>
                new AuditoriaEntidad();

            AuditManager.DefaultConfiguration
                .AuditEntryPropertyFactory = args =>
                new AuditoriaPropiedad();


            AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) =>
               (context as PrincipalDbContext).Auditorias.AddRange(audit.Entries.Cast<AuditoriaEntidad>());


            //3. 
            //Excluir todas las entidades
            AuditManager.DefaultConfiguration.Exclude(x => true);

            //Excluir todas las propiedades
            AuditManager.DefaultConfiguration.ExcludeProperty();

            //Incluir todas las propiedades
            //AuditManager.DefaultConfiguration.IncludeProperty();

            //Add Manual 
            //AuditManager.DefaultConfiguration.Include<ParametroSistema>();
            //AuditManager.DefaultConfiguration.IncludeProperty<ParametroSistema>(p => new { p.Valor });

            //4. Apply configuration
            AuditManager.DefaultConfiguration.ExcludeIncludeEntityPredicates.Add((x) =>
            {
                var entityType = x.GetType();

                //Check if dynamicProxies
                if (entityType.BaseType != null && entityType.Namespace == "System.Data.Entity.DynamicProxies")
                {
                    entityType = entityType.BaseType;
                }

                if (AuditedDictionary.ContainsKey(entityType.Name.ToUpper()))
                {
                    return (bool?)true;
                }
                return null;
            });

            AuditManager.DefaultConfiguration.ExcludeIncludePropertyPredicates.Add((x, s) =>
            {
                var entityType = x.GetType();

                //Check if dynamicProxies
                if (entityType.BaseType != null && entityType.Namespace == "System.Data.Entity.DynamicProxies")
                {
                    entityType = entityType.BaseType;
                }


                if (AuditedDictionary.ContainsKey(entityType.Name.ToUpper()))
                {
                    var propertys = AuditedDictionary[entityType.Name.ToUpper()];

                    if (propertys.ContainsKey(s.ToUpper()))
                        return (bool?)true;
                }

                return null;

            });


            //5. Ignore Events
            //TODO Comentar, para permitir registrar cambios en las relaciones. Ejemplo en un usuario se asocio un rol
            //AuditManager.DefaultConfiguration.IgnoreRelationshipAdded = true;
            //AuditManager.DefaultConfiguration.IgnoreRelationshipDeleted = true;

            // Globally 
            //Guardar siempre las propiedades incluso si no cambian...
            AuditManager.DefaultConfiguration.IgnorePropertyUnchanged = false;


            // Globally
            //When an entity satisfies the predicate, the audit entry state will be changed from "EntityModified" to either "EntitySoftAdded" or "EntitySoftDeleted"
            AuditManager.DefaultConfiguration.SoftDeleted<ISoftDelete>(x => x.IsDeleted);

        }


        public PrincipalDbContext()
        {
#if DEBUG
            //var factory = new NLogFactory();
            //log = factory.Create(typeof(PrincipalDbContext));
            //this.Database.Log = l => log.Debug(l);
#endif
        }

        public PrincipalDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Init();
        }


        private void Init()
        {

#if DEBUG
            this.Database.Log = l => this.Logger.Debug(l);
#endif

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            //Agregar configuraciones

            //Comun
            modelBuilder.Configurations.AddFromAssembly(typeof(FuncionalidadMap).Assembly);

            //Seguridad
            modelBuilder.Configurations.AddFromAssembly(typeof(UsuarioMap).Assembly);

            //TODO: Agregar los Mapeos Adicionales
            //modelBuilder.Configurations.AddFromAssembly(typeof(FooMap).Assembly);

            //Otros... 
            modelBuilder.Configurations.AddFromAssembly(typeof(TipoCatalogoMap).Assembly);

            modelBuilder.Entity<AuditEntry>()
                          .Map<AuditoriaEntidad>
                          (m => m.Requires("ObjectType").HasValue("AuditoriaEntidad"))
                          .ToTable("AuditoriaEntidad", "SCH_USUARIOS");

            modelBuilder.Entity<AuditEntryProperty>()
                    .Map<AuditoriaPropiedad>
                    (m => m.Requires("ObjectType").HasValue("AuditoriaPropiedad"))
                    .ToTable("AuditoriaPropiedad", "SCH_USUARIOS");

            //Desactivar eliminado en cascada por defecto
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }


        public override int SaveChanges()
        {

            var audit = new Audit();
            audit.PreSaveChanges(this);
            var rowAffecteds = base.SaveChanges();
            audit.PostSaveChanges();

            if (audit.Configuration.AutoSavePreAction != null)
            {
                audit.Configuration.AutoSavePreAction(this, audit);
                base.SaveChanges();
            }

            return rowAffecteds;
        }




        public override Task<int> SaveChangesAsync()
        {
            return SaveChangesAsync(CancellationToken.None);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var audit = new Audit();
            audit.PreSaveChanges(this);
            var rowAffecteds = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            audit.PostSaveChanges();

            if (audit.Configuration.AutoSavePreAction != null)
            {
                audit.Configuration.AutoSavePreAction(this, audit);
                await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }

            return rowAffecteds;
        }
    }
}
