﻿using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class DetalleReservaQrMap : EntityTypeConfiguration<DetalleReservaQr>
    {
        public DetalleReservaQrMap()
        {
            ToTable("detalles_reservas_qr", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");

            Property(d => d.ReservaHotelQrId).HasColumnName("reserva_hotel_qr_id");
            Property(d => d.ReservaHotelQrRef).HasColumnName("reserva_hotel_qr_ref");
            Property(d => d.OrigenConsumoId).HasColumnName("origen_consumo_id");
            Property(d => d.FechaReserva).HasColumnName("fecha_reserva");
            Property(d => d.FechaConsumo).HasColumnName("fecha_consumo");
            Property(d => d.Consumido).HasColumnName("consumido");
            Property(d => d.Facturado).HasColumnName("facturado");
            Property(d => d.LiquidacionDetalleId).HasColumnName("liquidacion_detalle_id");
        }
    }
}
