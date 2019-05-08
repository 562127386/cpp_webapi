using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class DetalleDistribucionMap : EntityTypeConfiguration<DetalleDistribucion>
    {
        public DetalleDistribucionMap()
        {
            ToTable("detalles_distribuciones", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.DistribucionViandaId).HasColumnName("DistribucionViandaId");
            Property(d => d.SolicitudViandaId).HasColumnName("SolicitudViandaId");
            Property(d => d.TotalAsignado).HasColumnName("total_asignado");
            Property(d => d.TotalEntregado).HasColumnName("total_entregado");
           
            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");


            this.HasRequired(t => t.DistribucionVianda)
                .WithMany()
                .HasForeignKey(d => d.DistribucionViandaId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.SolicitudVianda)
                .WithMany()
                .HasForeignKey(d => d.SolicitudViandaId)
                .WillCascadeOnDelete(false);
            
        }
    }
}
