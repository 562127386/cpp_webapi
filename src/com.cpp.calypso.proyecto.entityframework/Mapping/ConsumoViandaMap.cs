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
    public class ConsumoViandaMap : EntityTypeConfiguration<ConsumoVianda>
    {
        public ConsumoViandaMap()
        {
            ToTable("consumos_viandas", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.SolicitudVianda)
                .WithMany()
                .HasForeignKey(s => s.SolicitudViandaId)
                .WillCascadeOnDelete(false);

            HasRequired(p => p.colaborador)
                .WithMany()
                .HasForeignKey(s => s.ColaboradorId)
                .WillCascadeOnDelete(false);

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.SolicitudViandaId).HasColumnName("solicitud_vianda_id");
            Property(d => d.ColaboradorId).HasColumnName("colaborador_id");

        }
    }
}
