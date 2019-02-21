using com.cpp.calypso.comun.dominio;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
 

namespace com.cpp.calypso.comun.entityframework
{

    public class ParametroOpcionMap :
        EntityTypeConfiguration<ParametroOpcion>
    {
        public ParametroOpcionMap()
        {
            ToTable("parametro_opciones", "SCH_USUARIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            
            HasRequired(p => p.Parametro).WithMany(s => s.Opciones).HasForeignKey(p => p.ParametroId);
 
        }
    }
}
