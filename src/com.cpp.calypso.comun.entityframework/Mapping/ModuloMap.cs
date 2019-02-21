

 
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Abp.EntityFramework.Extensions;
using System.Data.Entity.Infrastructure.Annotations;

namespace com.cpp.calypso.comun.entityframework
{
    public class ModuloMap :
        EntityTypeConfiguration<dominio.Modulo>
    {
        public ModuloMap()
        {
            ToTable("modulos", "SCH_USUARIOS");
            HasKey(d => d.Id);

            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            Property(d => d.Codigo)
            .HasColumnAnnotation("Index",
             new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
          
            
    
        }
    }
}
