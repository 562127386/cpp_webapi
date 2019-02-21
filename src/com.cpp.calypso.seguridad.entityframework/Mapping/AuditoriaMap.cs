using com.cpp.calypso.comun.dominio;
 
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.seguridad.entityframework
{


    public class AuditoriaMap :
      EntityTypeConfiguration<Auditoria>
    {
        public AuditoriaMap()
        {

            ToTable("auditorias", "SCH_USUARIOS");
            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

    
        }

       
    }
}
