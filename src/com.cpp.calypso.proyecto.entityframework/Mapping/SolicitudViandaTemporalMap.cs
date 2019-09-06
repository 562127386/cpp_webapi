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
   public  class SolicitudViandaTemporalMap : EntityTypeConfiguration<SolicitudViandaTemporal>
    {
        public SolicitudViandaTemporalMap()
        {
            ToTable("solicitudes_viandas_temporal", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.FechaSolicitud).HasColumnName("fecha_solicitud");
            Property(d => d.SolicitanteId).HasColumnName("solicitante_id");
            Property(d => d.TipoComidaId).HasColumnName("tipo_comida");
            Property(d => d.TotalConsumido).HasColumnName("total_consumido");
            Property(d => d.TotalPedido).HasColumnName("total_pedido");
            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");

        }
    }
}
