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
    public class DistribucionViandaMap : EntityTypeConfiguration<DistribucionVianda>
    {
        public DistribucionViandaMap()
        {
            ToTable("distribuciones_viandas", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.ConductorAsigandoId).HasColumnName("conductor_asignado_id");
            Property(d => d.Estado).HasColumnName("estado");
            Property(d => d.Fecha).HasColumnName("fecha");
            Property(d => d.HoraAsigancionTransporte).HasColumnName("hora_asigancion_transporte");
            Property(d => d.Liquidado).HasColumnName("liquidado");
            Property(d => d.ProveedorId).HasColumnName("ProveedorId");
            Property(d => d.TipoComidaId).HasColumnName("tipo_comida_id");
            Property(d => d.TotalEntregadoTransporte).HasColumnName("total_entregado_transporte");
            Property(d => d.TotalJustificado).HasColumnName("total_justificado");
            Property(d => d.TotalLiquidado).HasColumnName("total_liquidado");
            Property(d => d.TotalPedido).HasColumnName("total_pedido");

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");

        }
    }
}
