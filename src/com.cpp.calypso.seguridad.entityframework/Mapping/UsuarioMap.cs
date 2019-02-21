using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
 
using System.Data.Entity.Infrastructure.Annotations;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.seguridad.entityframework
{
    public class UsuarioMap :
        EntityTypeConfiguration<Usuario>
    {
         public UsuarioMap()
        {
            ToTable("usuarios", "SCH_USUARIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            Property(d => d.Cuenta)
                   .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute() { IsUnique = true }));

 
              // Relationships
            this.HasMany(t => t.Roles)
                .WithMany(t => t.Usuarios)
                .Map(m =>
                {
                    //m.HasTableAnnotation
                    m.ToTable("usuario_rol", "SCH_USUARIOS");
                    m.MapLeftKey("usuarioId");
                    m.MapRightKey("rolId");
                });

            //Modulos
            this.HasMany(t => t.Modulos)
               .WithMany(t => t.Usuarios)
               .Map(m =>
               {
                    //m.HasTableAnnotation
                    m.ToTable("usuario_modulo", "SCH_USUARIOS");
                   m.MapLeftKey("usuarioId");
                   m.MapRightKey("moduloId");
               });



        }
    }
}
