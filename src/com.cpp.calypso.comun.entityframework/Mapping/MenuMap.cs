using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
 
using System.Data.Entity.Infrastructure.Annotations;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.comun.entityframework
{
    public class MenuMap :
        EntityTypeConfiguration<Menu>
    {
         public MenuMap()
        {
            ToTable("menus", "SCH_USUARIOS");
            HasKey(d => d.Id);

            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            string uniqueIndex = "UX_MEN_SIS_ID_MEN_CODIGO";


            Property(d => d.Codigo)
                 .HasColumnAnnotation("Index",
                  new IndexAnnotation(new IndexAttribute(uniqueIndex) { IsUnique = true, Order = 2 }));


      

        }
    }
}
