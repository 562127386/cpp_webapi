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
    [Serializable]
    public class ConsumoViandaTemporalMap: EntityTypeConfiguration<ConsumoViandaTemporal>
    {

        public ConsumoViandaTemporalMap()
        {
            ToTable("consumos_viandas_temporal", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.ColaboradorId).HasColumnName("colaborador_id");
            Property(d => d.FechaConsumo).HasColumnName("fecha_consumo");
            Property(d => d.SolicitudViandaTemporalId).HasColumnName("solicitud_vianda_temporal_id");
            Property(d => d.OrigenConsumoId).HasColumnName("origen_consumo_id");
            Property(d => d.SolicitudViandaTemporalRef).HasColumnName("solicitud_vianda_temporal_ref");
            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");

        }
    }
}
