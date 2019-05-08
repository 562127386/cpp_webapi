using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class ConsumoViandaQrMap : EntityTypeConfiguration<ConsumoViandaQr>
    {
        public ConsumoViandaQrMap()
        {
            ToTable("consumos_viandas_qr", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.SolicitudViandaId).HasColumnName("solicitud_vianda_id");
            Property(d => d.FechaConsumoVianda).HasColumnName("fecha_consumo_vianda");
            Property(d => d.Identificacion).HasColumnName("identificacion");
            Property(d => d.OrigenConsumoId).HasColumnName("origen_consumo_id");

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");


            this.HasRequired(t => t.SolicitudVianda)
                .WithMany()
                .HasForeignKey(d => d.SolicitudViandaId)
                .WillCascadeOnDelete(false);
        }
    }
}
