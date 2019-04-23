using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class ConsumoMap : EntityTypeConfiguration<Consumo>
    {
        public ConsumoMap()
        {
            ToTable("consumos", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.TipoComidaId).HasColumnName("tipo_comida_id");
            Property(d => d.OpcionComidaId).HasColumnName("opcion_comida");
            Property(d => d.ColaboradorId).HasColumnName("colaborador_id");
            Property(d => d.ProveedorId).HasColumnName("proveedor_id");
            Property(d => d.Fecha).HasColumnName("fecha");
            Property(d => d.Observacion).HasColumnName("observacion");
            Property(d => d.OrigenConsumoId).HasColumnName("origen_consumo_id");
            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");

            this.HasRequired(t => t.Proveedor)
                .WithMany()
                .HasForeignKey(d => d.ProveedorId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Colaborador)
                .WithMany()
                .HasForeignKey(d => d.ColaboradorId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.TipoComida)
                .WithMany()
                .HasForeignKey(d => d.TipoComidaId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.OpcionComida)
                .WithMany()
                .HasForeignKey(d => d.OpcionComidaId)
                .WillCascadeOnDelete(false); ;
        }
    }
}
