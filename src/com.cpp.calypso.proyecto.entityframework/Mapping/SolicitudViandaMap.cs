using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class SolicitudViandaMap : EntityTypeConfiguration<SolicitudVianda>
    {
        public SolicitudViandaMap()
        {
            ToTable("solicitudes_viandas", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.Id).HasColumnName("id");
            Property(d => d.LocacionId).HasColumnName("locacion_id");
            Property(d => d.AlcanceViandas).HasColumnName("alcance_viandas");
            Property(d => d.AnotadorId).HasColumnName("anotador_id");
            Property(d => d.AreaId).HasColumnName("area_id");
            Property(d => d.Consumido).HasColumnName("consumido");
            Property(d => d.ConsumoJustificado).HasColumnName("consumo_justificado");
            //Property(d => d.CreationTime).HasColumnName("consumo_justificado");
            Property(d => d.DisciplinaId).HasColumnName("disciplina_id");
            Property(d => d.Estado).HasColumnName("estado");
            Property(d => d.FechaAlcancce).HasColumnName("fecha_alcancce");
            Property(d => d.FechaSolicitud).HasColumnName("fecha_solicitud");
            Property(d => d.HoraEntregaRestaurante).HasColumnName("hora_entrega_restaurante");
            Property(d => d.HoraEntregaTransportista).HasColumnName("hora_entrega_transportista");
            Property(d => d.Observaciones).HasColumnName("observaciones");
            Property(d => d.PedidoViandas).HasColumnName("pedido_viandas");
            Property(d => d.ReferenciaUbicacion).HasColumnName("referencia_ubicacion");
            Property(d => d.SolicitanteId).HasColumnName("solicitante_id");
            Property(d => d.SolicitudOriginalId).HasColumnName("solicitud_original_id");
            Property(d => d.TipoComidaId).HasColumnName("tipo_comida_id");
            Property(d => d.TotalConsumido).HasColumnName("total_consumido");
            Property(d => d.TotalPedido).HasColumnName("total_pedido");

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");

            this.HasRequired(t => t.Solicitante)
                .WithMany()
                .HasForeignKey(d => d.SolicitanteId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Locacion)
                .WithMany()
                .HasForeignKey(d => d.LocacionId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.TipoComida)
                .WithMany()
                .HasForeignKey(d => d.TipoComidaId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Disciplina)
                .WithMany()
                .HasForeignKey(d => d.DisciplinaId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Area)
                .WithMany()
                .HasForeignKey(d => d.AreaId)
                .WillCascadeOnDelete(false);

            this.HasOptional(t => t.SolicitudOriginal)
                .WithMany()
                .HasForeignKey(d => d.SolicitudOriginalId)
                .WillCascadeOnDelete(false);

            this.HasOptional(t => t.Anotador)
                .WithMany()
                .HasForeignKey(d => d.AnotadorId)
                .WillCascadeOnDelete(false);
        }
    }
}
