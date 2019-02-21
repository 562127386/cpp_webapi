using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
 
using System.Data.Entity.Infrastructure.Annotations;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.comun.entityframework
{
    public class MenuItemMap :
        EntityTypeConfiguration<MenuItem>
    {
        public MenuItemMap()
        {
            ToTable("menuitems", "SCH_USUARIOS");
            HasKey(d => d.Id);
            Property(d => d.Id) 
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            string uniqueIndex = "UX_MIT_MEN_ID_MIT_CODIGO";

            Property(d => d.MenuId).HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute(uniqueIndex) { IsUnique = true, Order = 1 }));

            Property(d => d.Codigo).HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute(uniqueIndex) { IsUnique = true, Order = 2 }));
     
            // Relationships
            this.HasRequired(t => t.Menu)
                .WithMany(t => t.Items)
                .HasForeignKey(d => d.MenuId);

            this.
                HasOptional(e => e.Padre).
                WithMany().
                HasForeignKey(m => m.PadreId);

            //TODO: Mapper 
            //this.HasOptional(e => e.Funcionalidad).WithOptionalDependent().Map(d => d.MapKey("FUN_ID")); //.Map( d => d.MapKey("jc"));
 

        }
    }
}
