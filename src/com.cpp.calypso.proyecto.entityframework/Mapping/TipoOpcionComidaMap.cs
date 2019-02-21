using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.cpp.calypso.proyecto.dominio.Entidades;

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


            HasRequired(p => p.opcion_comida)
                .WithMany()
                .HasForeignKey(s => s.opcion_comida_id)
                .WillCascadeOnDelete(false);

            HasRequired(p => p.tipo_comida)
                .WithMany()
                .HasForeignKey(s => s.tipo_comida_id)
                .WillCascadeOnDelete(false);

            // Relationships
            this.HasRequired(t => t.contrato)
                .WithMany(t => t.tipo_opciones_comida)
                .HasForeignKey(d => d.ContratoId);

            Property(d => d.ContratoId).HasColumnName("contrato_id");
            Property(d => d.IsDeleted).HasColumnName("vigente");

        }
    }
}
