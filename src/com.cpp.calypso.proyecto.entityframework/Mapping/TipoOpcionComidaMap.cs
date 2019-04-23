using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class TipoOpcionComidaMap : EntityTypeConfiguration<TipoOpcionComida>
    {
        public TipoOpcionComidaMap()
        {
            ToTable("tipos_opciones_comidas", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.ContratoId).HasColumnName("contrato_id");
            Property(d => d.Costo).HasColumnName("costo");
            Property(d => d.OpcionComidaId).HasColumnName("opcion_comida_id");
            Property(d => d.TipoComidaId).HasColumnName("tipo_comida_id");


            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");

            this.HasRequired(t => t.Contrato)
                .WithMany()
                .HasForeignKey(d => d.ContratoId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.OpcionComida)
                .WithMany()
                .HasForeignKey(d => d.OpcionComidaId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.TipoComida)
                .WithMany()
                .HasForeignKey(d => d.TipoComidaId)
                .WillCascadeOnDelete(false);
        }
    }
}
