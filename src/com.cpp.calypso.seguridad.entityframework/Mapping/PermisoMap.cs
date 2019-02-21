using com.cpp.calypso.comun.dominio;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
 

namespace com.cpp.calypso.comun.entityframework
{


    public class PermisoMap :
       EntityTypeConfiguration<Permiso>
    {
        public PermisoMap()
        {
            ToTable("permisos", "SCH_USUARIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

          
            HasRequired(a => a.Accion)
                .WithMany()
                .HasForeignKey(u => u.AccionId);

            //TODO:
            //HasRequired(a => a.Accion).WithRequiredPrincipal().
            // HasRequired(a => a.Accion).WithRequiredDependent(); 
            //(f => f..WithMany(f => f.Acciones).HasForeignKey(a => a.FuncionalidadId);
 
        }
    }
}
