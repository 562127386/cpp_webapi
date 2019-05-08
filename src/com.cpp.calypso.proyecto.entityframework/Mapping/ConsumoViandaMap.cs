using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class ConsumoViandaMap : EntityTypeConfiguration<ConsumoVianda>
    {
        public ConsumoViandaMap()
        {
            ToTable("consumos_viandas", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.SolicitudViandaId).HasColumnName("solicitud_vianda_id");
            Property(d => d.ColaboradorId).HasColumnName("colaborador_id");
            Property(d => d.FechaConsumoVianda).HasColumnName("fecha_consumo_vianda");
            Property(d => d.EnSitio).HasColumnName("en_sitio");
            Property(d => d.Observaciones).HasColumnName("observaciones");
            Property(d => d.OrigenConsumoId).HasColumnName("origen_consumo_id");

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");


            this.HasRequired(t => t.SolicitudVianda)
                .WithMany()
                .HasForeignKey(d => d.SolicitudViandaId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Colaborador)
                .WithMany()
                .HasForeignKey(d => d.ColaboradorId)
                .WillCascadeOnDelete(false);
        }
    }
}
