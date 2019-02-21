using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
 
using System.Data.Entity.Infrastructure.Annotations;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.comun.entityframework
{

    public class ParametroMap :
        EntityTypeConfiguration<ParametroSistema>
    {
        public ParametroMap()
        {
            ToTable("parametros", "SCH_USUARIOS");
            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            string uniqueIndex = "UX_PAR_SIS_ID_PAR_CODIGO";


            Property(d => d.Codigo)
                 .HasColumnAnnotation("Index",
                  new IndexAnnotation(new IndexAttribute(uniqueIndex) { IsUnique = true, Order = 2 }));

        

        }
    }
}
