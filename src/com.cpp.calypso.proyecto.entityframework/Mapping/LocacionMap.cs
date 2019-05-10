using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class LocacionMap : EntityTypeConfiguration<Locacion>
    {
        public LocacionMap()
        {
            ToTable("locaciones", "SCH_CATALOGOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.ZonaId).HasColumnName("zona_id");
            Property(d => d.Codigo).HasColumnName("codigo");
            Property(d => d.Nombre).HasColumnName("nombre");

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");


            this.HasRequired(t => t.Zona)
                .WithMany()
                .HasForeignKey(d => d.ZonaId)
                .WillCascadeOnDelete(false);
        }
    }
}
