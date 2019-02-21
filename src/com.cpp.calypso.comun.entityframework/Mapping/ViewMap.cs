
using com.cpp.calypso.comun.dominio;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;


namespace com.cpp.calypso.comun.entityframework
{
    public class ViewMap :
        EntityTypeConfiguration<View>
    {
        public ViewMap()
        {
             ToTable("views", "SCH_USUARIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            Property(d => d.Name)
            .HasColumnAnnotation("Index",
             new IndexAnnotation(new IndexAttribute() { IsUnique = true }));

           
            Ignore(d => d.Layout);
            Ignore(d => d.ModelType);

        }
    }
}
