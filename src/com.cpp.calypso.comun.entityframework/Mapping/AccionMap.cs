using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using com.cpp.calypso.comun.dominio;

namespace com.cpp.calypso.comun.entityframework
{
    public class AccionMap :
        EntityTypeConfiguration<Accion>
    {


        public AccionMap()
        {
            ToTable("acciones", "SCH_USUARIOS");
            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            string uniqueIndex = "UX_ACC_FUN_ID_ACC_CODIGO";

            Property(d => d.FuncionalidadId).HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute(uniqueIndex) { IsUnique = true, Order = 1 }));

            Property(d => d.Codigo)
                   .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute(uniqueIndex) { IsUnique = true, Order = 2 }));


            HasRequired(a => a.Funcionalidad).WithMany(f => f.Acciones).HasForeignKey(a => a.FuncionalidadId);


        }
    }
}
