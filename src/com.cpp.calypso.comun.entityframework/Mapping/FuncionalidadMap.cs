using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
 
using System.Data.Entity.Infrastructure.Annotations;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.comun.entityframework
{

    public class FuncionalidadMap :
        EntityTypeConfiguration<Funcionalidad>
    {
        public FuncionalidadMap()
        {
            ToTable("funcionalidades", "SCH_USUARIOS");
         
            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            string uniqueIndex = "UX_FUN_SIS_ID_FUN_CODIGO";


            Property(d => d.Codigo)
                .HasColumnAnnotation("Index",
                  new IndexAnnotation(new IndexAttribute(uniqueIndex) { IsUnique = true, Order = 2 }));


         
            HasRequired(a => a.Modulo).WithMany(f => f.Funcionalidades).HasForeignKey(a => a.ModuloId);
 
        }
    }
}
