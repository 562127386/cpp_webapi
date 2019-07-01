using com.cpp.calypso.proyecto.dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace com.cpp.calypso.proyecto.entityframework.Mapping
{
    public class ReservaHotelQrMap : EntityTypeConfiguration<ReservaHotelQr>
    {
        public ReservaHotelQrMap()
        {
            ToTable("reservas_hoteles_qr", "SCH_SERVICIOS");

            HasKey(d => d.Id);
            Property(d => d.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            Property(d => d.EspacioHabitacionId).HasColumnName("espacio_habitacion_id");
            Property(d => d.IdentificacionColaborador).HasColumnName("identificacion_colaborador");
            Property(d => d.FechaRegistro).HasColumnName("fecha_registro");
            Property(d => d.FechaDesde).HasColumnName("fecha_desde");
            Property(d => d.FechaHasta).HasColumnName("fecha_hasta");

            Property(d => d.IsDeleted).HasColumnName("vigente");
            Property(d => d.Version).HasColumnName("m_version");
            Property(d => d.Ref).HasColumnName("m_ref");
            Property(d => d.CreationTime).HasColumnName("creation_time");
            Property(d => d.CreatorUserId).HasColumnName("creator_user_id");
        }
    }
}
