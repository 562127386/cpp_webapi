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
    public class ConsumoViandaQrMap : EntityTypeConfiguration<ConsumoViandaQr>
    {
        public ConsumoViandaQrMap()
        {
            ToTable("consumos_viandas_qr", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.SolicitudVianda)
                .WithMany()
                .HasForeignKey(s => s.SolicitudViandaId)
                .WillCascadeOnDelete(false);


            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.SolicitudViandaId).HasColumnName("solicitud_vianda_id");
        }
    }
}
